using WorkflowSystem.Domain;
using WorkflowSystem.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowSystem.Dictionaries;

namespace WorkflowSystem.Data
{
    public class ProductRepository : Repository<Product, MainDatabaseContext>, IProductRepository
    {
        public ProductRepository(MainDatabaseContext context) : base(context)
        { }

        public virtual List<ProductListDTO> GetAll(ProductType productType)
        {
            return Context.Products.Select(x => new ProductListDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Image = Convert.ToBase64String(x.ImageTumb),
                ProductType = x.ProductType
            }).Where(x => x.ProductType == productType).ToList();
        }
        public virtual List<ProductListDTO> GetAllToMenu(ProductType productType)
        {
            return Context.Products.Select(x => new ProductListDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Image = Convert.ToBase64String(x.Image),
                ProductType = x.ProductType
            }).Where(x => x.ProductType == productType).ToList();
        }

    }
}
