using WorkflowSystem.Application.Abstraction;
using WorkflowSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WorkflowSystem.Data;
using WorkflowSystem.Dictionaries;

namespace WorkflowSystem.Application
{
    public interface IProductService : IService
    {
        void Add(ProductAddVM model, ProductType productType);
        List<ProductListDTO> GetProducts(ProductType produtType);
        List<ProductListDTO> GetProductsToMenu(ProductType produtType);
        ProductEditVM GetProduct(int id);
        void Edit(ProductEditVM model);
        void Delete(int id);
    }
}