using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Data
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<OrderListDTO> GetOrders();
        List<int> GetOrdersIds();
        List<OrderListDTO> GetDrinks();
    }
}
