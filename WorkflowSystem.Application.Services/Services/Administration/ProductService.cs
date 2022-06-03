using WorkflowSystem.Application.Abstraction;
using WorkflowSystem.Data;
using WorkflowSystem.Dictionaries;
using WorkflowSystem.Domain;
using WorkflowSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace WorkflowSystem.Application
{
    public class ProductService : ServiceBase, IProductService
    {

        public IProductRepository _productRepository { get; set; }

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = _=productRepository;
        }

        public virtual void Add(ProductAddVM model, ProductType productType)
        {
            Product product = new Product()
            {
                Description = model.Description,
                Name = model.Name,
                Price = model.Price,
                ProductType = productType
            };
            try
            {
                var bytes = Convert.FromBase64String(model.Image);
                var contents = new MemoryStream(bytes);
                Image image = Image.FromStream(contents);
                Image thumb = image.GetThumbnailImage(225, 150, () => false, IntPtr.Zero);
                var ms = new MemoryStream();
                thumb.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                product.ImageTumb = ms.ToArray();

                Image thumb2 = ResizeImage(image, 800, 470);
                var ms2 = new MemoryStream();
                thumb2.Save(ms2, ImageFormat.Png);
                ms2.Position = 0;
                product.Image = ms2.ToArray();
            }
            catch (Exception e)
            {
                product.Image = Convert.FromBase64String(model.Image);
                product.ImageTumb = Convert.FromBase64String(model.Image);
            }
            _productRepository.Add(product);
            _productRepository.Save();
        }
        public virtual List<ProductListDTO>GetProducts(ProductType productType)
        {
            return _productRepository.GetAll(productType);
        }
        public virtual List<ProductListDTO> GetProductsToMenu(ProductType productType)
        {
            return _productRepository.GetAllToMenu(productType);
        }
        public virtual ProductEditVM GetProduct(int id) {

            Product product = _productRepository.GetSingle(x => x.Id == id);
            ProductEditVM model = new ProductEditVM()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = Convert.ToBase64String(product.Image)
            };
            return model;
        }
        public virtual void Edit(ProductEditVM model)
        {
            Product product = _productRepository.GetSingle(x => x.Id == model.Id);

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;

            try
            {
                var bytes = Convert.FromBase64String(model.Image);
                var contents = new MemoryStream(bytes);
                Image image = Image.FromStream(contents);
                Image thumb = image.GetThumbnailImage(225, 150, () => false, IntPtr.Zero);
                var ms = new MemoryStream();
                thumb.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                product.ImageTumb = ms.ToArray();

                Image thumb2 = ResizeImage(image, 800, 470);
                var ms2 = new MemoryStream();
                thumb2.Save(ms2, ImageFormat.Png);
                ms2.Position = 0;
                product.Image = ms2.ToArray();
            }
            catch (Exception e)
            {
                product.Image = Convert.FromBase64String(model.Image);
                product.ImageTumb = Convert.FromBase64String(model.Image);
            }
            _productRepository.Edit(product);
            _productRepository.Save();
        }

        public virtual void Delete(int id)
        {
            Product product = _productRepository.GetSingle(x => x.Id == id);
            _productRepository.Delete(product);
            _productRepository.Save();
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, result.Width, result.Height);
            }
            return result;
        }

    }
}
