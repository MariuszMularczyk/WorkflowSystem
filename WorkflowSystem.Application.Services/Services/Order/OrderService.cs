using WorkflowSystem.Data;
using WorkflowSystem.Dictionaries;
using WorkflowSystem.Domain;
using WorkflowSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Application
{
    public class OrderService : ServiceBase, IOrderService
    {
        #region Dependencies
        public IOrderRepository _orderRepository { get; set; }
        public IProductRepository _productRepository { get; set; }
        public IProductOrderRepository _productOrderRepository { get; set; }
        public IAppSettingsRepository _appSettingsRepository { get; set; }
        #endregion
        public OrderService(IProductRepository productRepository, IAppSettingsRepository appSettingsRepository, IProductOrderRepository productOrderRepository, IOrderRepository orderRepository )
        {
            _productRepository = productRepository;
            _appSettingsRepository = appSettingsRepository;
            _productOrderRepository = productOrderRepository;
            _orderRepository = orderRepository;
            
        }

        public List<OrderListDTO> Add(OrderAddVM model)
        {

            Order order = new Order()
            {
                Table = model.Table,
                OrderStatus = OrderStatusEnum.Awaiting,
                TimeOfOrder = DateTime.Now
            };

            _orderRepository.Add(order);
            _orderRepository.Save();
            foreach(var item in model.ProductOrders)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    ProductOrder productOrder = new ProductOrder()
                    {
                        Order = order,
                        Product = _productRepository.GetSingle(x => x.Id == item.ProductId),
                    };
                    _productOrderRepository.Add(productOrder);
                }
                
            }

            _productOrderRepository.Save();

            _orderRepository.Edit(order);
            _orderRepository.Save();
            return GetOrders();
        }

        public virtual List<OrderListDTO> GetOrders()
        {
            return _orderRepository.GetOrders();
        }
        public virtual List<OrderListDTO> GetDrinks()
        {
            return _orderRepository.GetDrinks();
        }
        public virtual List<OrderListDTO> SetStatus(int orderId)
        {

            Order order = _orderRepository.GetSingle(x => x.Id == orderId);

            order.OrderStatus = OrderStatusEnum.Done;


            _orderRepository.Edit(order);
            _orderRepository.Save();

            return GetOrders();

        }

    }
}
