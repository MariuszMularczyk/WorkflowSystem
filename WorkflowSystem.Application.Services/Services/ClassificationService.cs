using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WorkflowSystem.Data;
using TextClustering;
using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.Text;

namespace WorkflowSystem.Application
{

    public class Prediction
    {
        [ColumnName("PredictedLabel")]
        public uint Cluster { get; set; }
    }
    class Prediction2
    {
        // Original label.
        public uint Label { get; set; }
        // Predicted label from the trainer.
        public uint PredictedLabel { get; set; }
    }
    public class TextData
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    class DataPoint
    {
        public uint Label { get; set; }
        [VectorType(20)]
        public string[] Features { get; set; }
    }



    public class InvPrediction
    {
        [ColumnName("PredictedLabel")]
        public int Class;
    }
    public class ClassificationService : ServiceBase, IClassificationService
    {
        private readonly IInvRepository _invRepository;
        private readonly IInvPositionRepository _invPositionRepository;
        public IRoleRepository AppRoleRepository { get; set; }


        public ClassificationService(IInvRepository invRepository, IInvPositionRepository invPositionRepository)
        {
            _invRepository = invRepository;
            _invPositionRepository = invPositionRepository;

            List<Inv> list = _invRepository.GetAll().ToList();
            List<InvData> documents = new List<InvData>();
            foreach (var item in list)
                documents.AddRange(item.Positions.Select(x => new InvData() { Id = x.Id, Class = x.Class, Description = x.Descryption }));
            int clusters = documents.Max(x => x.Class);
            if(documents.Count > 0)
            {
                Train(documents);
            }
            /*Train(documents.Take(30000));
            Evaluate(documents.TakeLast(2153));*/
        }

        private static MLContext _mlContext;
        private static PredictionEngine<InvData, InvPrediction> _predEngine;
        private static ITransformer _trainedModel;
        static IDataView _trainingDataView;

        public void Train(IEnumerable<InvData> data)
        {
            _mlContext = new MLContext(seed: 0);

            _trainingDataView = _mlContext.Data.LoadFromEnumerable(data);

            var pipeline = ProcessData();


            var trainingPipeline = BuildAndTrainModel(_trainingDataView, pipeline);

            //SaveModelAsFile(_mlContext, trainingDataViewSchema, _trainedModel, "");

            InvData inv = new InvData()
            {
                Id = 999999,
                Description = "Disney MSFR190-01B Analog Watch  - For Boys, Girls"
            };

            var test = Predict(inv);
        }
        public static IEstimator<ITransformer> ProcessData()
        {
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "Class", outputColumnName: "Label")
                            .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Description", outputColumnName: "Features"))
                            .AppendCacheCheckpoint(_mlContext);

            return pipeline;
        }

        public static IEstimator<ITransformer> BuildAndTrainModel(IDataView trainingDataView, IEstimator<ITransformer> pipeline)
        {

            var trainingPipeline = pipeline.Append(_mlContext.MulticlassClassification.Trainers.SdcaNonCalibrated("Label", "Features"))
                    .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
 
            _trainedModel = trainingPipeline.Fit(trainingDataView);
           
            _predEngine = _mlContext.Model.CreatePredictionEngine<InvData, InvPrediction>(_trainedModel);

            return trainingPipeline;

        }
        public int Predict(InvData inv)
        {
            var prediction = _predEngine.Predict(inv);
            return prediction.Class;
        }
        private static void SaveModelAsFile(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model, string path)
        {
            mlContext.Model.Save(model, trainingDataViewSchema, path);
        }

        /*private PredictionEngine<InvPosition, Prediction> _predictionEngine;

        public void Train(IEnumerable<InvPosition> data, int numberOfClusters)
        {
            var mlContext = new MLContext();
            var textDataView = mlContext.Data.LoadFromEnumerable(data);
            var textEstimator = mlContext.Transforms.Text.NormalizeText("Descryption")
                                .Append(mlContext.Transforms.Text.TokenizeIntoWords("Descryption"))
                                .Append(mlContext.Transforms.Text.RemoveDefaultStopWords("Descryption"))
                                .Append(mlContext.Transforms.Conversion.MapValueToKey("Descryption"))
                                .Append(mlContext.Transforms.Text.ProduceNgrams("Descryption"))
                                .Append(mlContext.Transforms.NormalizeLpNorm("Descryption"))
                                .Append(mlContext.MulticlassClassification.Trainers.NaiveBayes());
            var model = textEstimator.Fit(textDataView);
            _predictionEngine = mlContext.Model.CreatePredictionEngine<InvPosition, Prediction>(model);
        }*/

/*        public uint Predict(InvPosition position)
        {
            return _predictionEngine.Predict(position).Cluster;
        }*/
        public string Test()
        {
            return "test";
        }

        public static void Evaluate(IEnumerable<InvData>  data)
        {
            // STEP 5:  Evaluate the model in order to get the model's accuracy metrics
            Console.WriteLine($"=============== Evaluating to get model's accuracy metrics - Starting time: {DateTime.Now.ToString()} ===============");

            //Load the test dataset into the IDataView
            // <SnippetLoadTestDataset>
            var testDataView = _mlContext.Data.LoadFromEnumerable(data);
            // </SnippetLoadTestDataset>

            //Evaluate the model on a test dataset and calculate metrics of the model on the test data.
            // <SnippetEvaluate>
            var testMetrics = _mlContext.MulticlassClassification.Evaluate(_trainedModel.Transform(testDataView));
            // </SnippetEvaluate>

            StringBuilder test = new StringBuilder();
            
            // <SnippetDisplayMetrics>
            test.AppendLine($"*************************************************************************************************************");
            test.AppendLine($"*       Metryki pomiarowe dla metody klasyfikacji - LbfgsMaximumEntropy     ");
            test.AppendLine($"*------------------------------------------------------------------------------------------------------------");
            test.AppendLine($"*       MicroAccuracy:    {testMetrics.MicroAccuracy:0.###}");
            test.AppendLine($"*       MacroAccuracy:    {testMetrics.MacroAccuracy:0.###}");
            test.AppendLine($"*       LogLoss:          {testMetrics.LogLoss:0.###}");
            test.AppendLine($"*       LogLossReduction: {testMetrics.LogLossReduction:0.###}");
            test.AppendLine($"*************************************************************************************************************");

            string test2 = test.ToString();

        }

    }
}
