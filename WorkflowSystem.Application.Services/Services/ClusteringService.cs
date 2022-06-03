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
using HdbscanSharp.Runner;
using StopWord;
using HdbscanSharp.Distance;
using NickStats;

namespace WorkflowSystem.Application
{
    public class ClusteringResult<T>
    {
        public List<List<T>> Clusters { get; set; }
        public List<T> Unclassified { get; set; }
    }
    public class Word
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int NumberOfTextWhereTheWordAppears { get; set; }
        public int Frequency { get; set; }
    }

    public class ClusteringService : ServiceBase, IClusteringService
    {
        private readonly IInvRepository _invRepository;
        private readonly IInvPositionRepository _invPositionRepository;

        public ClusteringService(IInvRepository invRepository, IInvPositionRepository invPositionRepository)
        {
            _invRepository = invRepository;
            _invPositionRepository = invPositionRepository;
        }

        public void ClusterAndDetect()
        {
            List<InvPosition> documents = _invPositionRepository.GetAll().ToList();

            var result = documents.ClusterBy(document => document.Descryption, options => options
                .WithMinClusterSize(5) // The minimum cluster size (default value: 5, but you should change it)
                .WithMinWordLength(3) // The minimum word length
                .WithMaxPresencePercent(100) // The maximum overall presence in percent of one word among all text
                .UseCaching(true) // (optional, true by default. Will use more ram, but prevent redoing the same calculation multiple times)
                .WithMaxDegreeOfParallelism(Environment.ProcessorCount) // (optional, will use one thread by default)
                .WithLanguages(TextClustering.Language.English, TextClustering.Language.Polish) // (optional, will use English stop words if not specified) This is used to eliminate words that are so commonly used that they carry very little useful information.
            );

            List<InvPosition> listUnclassified = result.Unclassified; // List<InvPosition>
            List<List<InvPosition>> listClusters = result.Clusters; // List<List<InvPosition>>
            int classNumber = 0;
            foreach (var item in listClusters)
            {
                classNumber++;
                decimal[] x = Array.Empty<decimal>();
                foreach (var item2 in item)
                {
                    x = x.Append(item2.NetValueUnit).ToArray();
                }
                Array.Sort(x);
                var q1 = NStats.Percentile(x, 0.25F);
                var q3 = NStats.Percentile(x, 0.75F);
                var iqr = q3 - q1;

                var min = q1 - 1.5m * iqr;
                var max = q3 + 1.5m * iqr;

                foreach (var item2 in item)
                {
                    if(item2.NetValueUnit < min || item2.NetValueUnit > max)
                    {
                        item2.OverPaid = true;
                    }
                    item2.Class = classNumber;  
                }
                _invPositionRepository.EditRange(item);
            }
            _invPositionRepository.Save();

        }

        internal decimal Percentile(decimal[] sortedData, decimal p)
        {
            // algo derived from Aczel pg 15 bottom
            if (p >= 100.0m) return sortedData[sortedData.Length - 1];

            decimal position = (sortedData.Length + 1) * p / 100.0m;
            decimal leftNumber = 0.0m, rightNumber = 0.0m;

            decimal n = p / 100.0m * (sortedData.Length - 1) + 1.0m;

            if (position >= 1)
            {
                leftNumber = sortedData[(int)Math.Floor(n) - 1];
                rightNumber = sortedData[(int)Math.Floor(n)];
            }
            else
            {
                leftNumber = sortedData[0]; // first data
                rightNumber = sortedData[1]; // first data
            }

            //if (leftNumber == rightNumber)
            if (Equals(leftNumber, rightNumber))
                return leftNumber;
            decimal part = n - Math.Floor(n);
            return leftNumber + part * (rightNumber - leftNumber);
        } // end of internal function percentile


        internal char[] WordsSeparator { get; private set; } =
        {
            ' ', '.', '?', '\n', '\r', '\t', ',', '!', '(', ')', ';', '"', ':', '-', '\\', '/', '[', ']', '{', '}', '0',
            '1', '2', '3', '4', '5', '6', '7', '8', '9', '\''
        };

        /* public ClusteringResult<T> Cluster<T>(this IEnumerable<T> items,
             Func<T, string> getTextFunc )
         {


             var list = items as List<T> ?? items.ToList();
             var knownWords = ExtractExpressions(list.Select(getTextFunc));
             var vectors = list.Select(item => GenerateExpressionVector(knownWords, getTextFunc(item)));

             var result = HdbscanRunner.Run(new HdbscanParameters<Dictionary<int, int>>
             {
                 DataSet = vectors.ToArray(),
                 MinPoints = 1,
                 MinClusterSize = 1,
                 DistanceFunction = new CosineSimilarity(true, true),
                 MaxDegreeOfParallelism = Environment.ProcessorCount,
                 CacheDistance = true
             });

             // Read results.
             var labels = result.Labels;
             var n = labels.Max();

             var clusteringResult = new ClusteringResult<T>
             {
                 Clusters = new List<List<T>>(),
                 Unclassified = new List<T>()
             };

             for (var iCluster = 0; iCluster <= n; iCluster++)
             {
                 List<T> clustersItems = null;
                 for (var i = 0; i < labels.Length; i++)
                 {
                     if (labels[i] == iCluster)
                     {
                         var item = list[i];
                         if (clustersItems == null)
                             clustersItems = new List<T>();
                         clustersItems.Add(item);
                     }
                 }

                 if (clustersItems == null)
                     continue;

                 if (iCluster == 0)
                     clusteringResult.Unclassified.AddRange(clustersItems);
                 else
                     clusteringResult.Clusters.Add(clustersItems);
             }

             return clusteringResult;
         }*/

        /* private Dictionary<string, Word> ExtractExpressions(IEnumerable<string> textContents)
         {
             var knownWords = new Dictionary<string, Word>();
             var wordId = 1;
             var textContentsCount = 0;

             foreach (var textContent in textContents)
             {
                 textContentsCount++;

                 var wordsFreq = new Dictionary<string, int>();
                 var words = textContent.Split(WordsSeparator, StringSplitOptions.RemoveEmptyEntries);

                 foreach (var word in words)
                 {
                     var key = word.ToLower();

                     if (key.Length < 3)
                         continue;

                     if (wordsFreq.ContainsKey(key))
                         wordsFreq[key]++;
                     else
                         wordsFreq.Add(key, 1);
                 }

                 foreach (var wordFreq in wordsFreq)
                 {
                     if (knownWords.ContainsKey(wordFreq.Key))
                     {
                         knownWords[wordFreq.Key].NumberOfTextWhereTheWordAppears++;
                         knownWords[wordFreq.Key].Frequency += wordFreq.Value;
                     }
                     else
                         knownWords.Add(wordFreq.Key, new Word
                         {
                             Id = wordId++,
                             Text = wordFreq.Key,
                             NumberOfTextWhereTheWordAppears = 1,
                             Frequency = wordFreq.Value
                         });
                 }
             }

             var maxPresenceCount = 100 * textContentsCount / 100;

             var stopWordList = new HashSet<string>(StopWords.GetStopWords("en").Select(x => x.ToLower()).Distinct());

             knownWords = knownWords
                 .Where(m => m.Value.NumberOfTextWhereTheWordAppears >= 5)
                 .Where(m => m.Value.NumberOfTextWhereTheWordAppears <= maxPresenceCount)
                 .Where(m => !stopWordList.Contains(m.Key))
                 .OrderBy(x => x.Value.NumberOfTextWhereTheWordAppears)
                 .ToDictionary(x => x.Key, x => x.Value);

             return knownWords;
         }*/

        /*        private Dictionary<int, int> GenerateExpressionVector(Dictionary<string, Word> knownWords,
                    string textContent)
                {
                    var freq = new Dictionary<string, int>();
                    var words = textContent.Split(WordsSeparator, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var word in words)
                    {
                        var key = word.ToLower();

                        if (!knownWords.ContainsKey(key))
                            continue;

                        if (freq.ContainsKey(key))
                            freq[key]++;
                        else
                            freq.Add(key, 1);
                    }

                    var vector = freq.ToDictionary(x => knownWords[x.Key].Id, x => x.Value);
                    return vector;
                }*/
    }

   
}
