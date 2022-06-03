using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowSystem.Dictionaries;

namespace WorkflowSystem.Data
{
    public interface IProductRepository : IRepository<Product>
    {
        List<ProductListDTO> GetAll(ProductType producType); 
        List<ProductListDTO> GetAllToMenu(ProductType producType); 
    }
}
