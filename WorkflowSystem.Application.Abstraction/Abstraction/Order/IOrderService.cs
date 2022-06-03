using WorkflowSystem.Application.Abstraction;
using WorkflowSystem.Domain;
using WorkflowSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WorkflowSystem.Data;

namespace WorkflowSystem.Application
{
    public interface IOrderService : IService
    {
        List<OrderListDTO> Add(OrderAddVM model);
        List<OrderListDTO> GetOrders();
        List<OrderListDTO> GetDrinks();
        List<OrderListDTO>  SetStatus(int orderId);

    }
}