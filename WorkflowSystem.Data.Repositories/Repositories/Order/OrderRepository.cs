using WorkflowSystem.Domain;
using WorkflowSystem.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowSystem.Data
{
    public class OrderRepository : Repository<Order, MainDatabaseContext>, IOrderRepository
    {
        public OrderRepository(MainDatabaseContext context) : base(context)
        { }



        public virtual List<OrderListDTO> GetOrders()
        {
            List<OrderListDTO> orders = new List<OrderListDTO>();

            List<OrderItemDTO> ordersId = Context.Orders.Select(x => new OrderItemDTO()
            {
                OrderId = x.Id,
                Table = x.Table,
                OrderStatus = x.OrderStatus,
                TimeOfOrder = x.TimeOfOrder,
            }).Where(x => x.OrderStatus != Dictionaries.OrderStatusEnum.Done).ToList();
            int OrderItemId = 1;
            foreach (OrderItemDTO item in ordersId)
            {

                List<ProductItemDTO> drinksb = Context.ProductsOrders.Select(x => new ProductItemDTO()
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    ProductType = x.Product.ProductType,
                    OrderId = x.OrderId,
                }).Where(x => x.OrderId == item.OrderId).ToList();


                List<ProductItemDTO> drinks = new List<ProductItemDTO>();

                foreach (ProductItemDTO product in drinksb)
                {
                    if (drinks.Any(x => x.ProductId == product.ProductId))
                    {
                        drinks.Where(x => x.ProductId == product.ProductId).Select(x => x.Quantity = x.Quantity + 1).ToList();
                    }
                    else
                    {
                        drinks.Add(new ProductItemDTO()
                        {
                            Quantity = 1,
                            ProductName = product.ProductName,
                            OrderId = product.OrderId,
                            ProductType = product.ProductType,
                            ProductId = product.ProductId
                        });
                    }
                }
;
                if (drinks.Any())
                {
                    StringBuilder stringDrinks = new StringBuilder("");
                    foreach (ProductItemDTO product in drinks)
                    {
                        stringDrinks.Append(product.Quantity);
                        stringDrinks.Append(" x ");
                        stringDrinks.Append(product.ProductName);
                        stringDrinks.Append(", \n");
                    }
                    OrderListDTO appetizers = new OrderListDTO()
                    {
                        OrderItemId = OrderItemId++,
                        OrderId = item.OrderId,
                        Table = item.Table,
                        Order = stringDrinks.ToString(),
                        TimeOfOrder = item.TimeOfOrder,
                        ProductList = drinks,
                    };
                    orders.Add(appetizers);
                }

            }
            return orders.OrderBy(x => x.TimeOfOrder).ToList();
        }


        public virtual List<OrderListDTO> GetDrinks()
        {
            List<OrderListDTO> orders = new List<OrderListDTO>();

            List<OrderItemDTO> ordersId = Context.Orders.Select(x => new OrderItemDTO()
            {
                OrderId = x.Id,
                Table = x.Table,
                OrderStatus = x.OrderStatus,
                TimeOfOrder = x.TimeOfOrder,
            }).Where(x => x.OrderStatus != Dictionaries.OrderStatusEnum.Done).ToList();
            int OrderItemId = 1;
            foreach (OrderItemDTO item in ordersId)
            {

                List<ProductItemDTO> drinksb = Context.ProductsOrders.Select(x => new ProductItemDTO()
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    ProductType = x.Product.ProductType,
                    OrderId = x.OrderId,
                }).Where(x => x.OrderId == item.OrderId).ToList();

                List<ProductItemDTO> drinks = new List<ProductItemDTO>();

                foreach (ProductItemDTO product in drinksb)
                {
                    if (drinks.Any(x => x.ProductId == product.ProductId))
                    {
                        drinks.Where(x => x.ProductId == product.ProductId).Select(x => x.Quantity = x.Quantity + 1).ToList();
                    }
                    else
                    {
                        drinks.Add(new ProductItemDTO()
                        {
                            Quantity = 1,
                            ProductName = product.ProductName,
                            OrderId = product.OrderId,
                            ProductType = product.ProductType,
                            ProductId = product.ProductId
                        });
                    }
                }

                if (drinks.Any())
                {
                    StringBuilder stringDrinks = new StringBuilder("");
                    foreach (ProductItemDTO product in drinks)
                    {
                        stringDrinks.Append(product.Quantity);
                        stringDrinks.Append(" x ");
                        stringDrinks.Append(product.ProductName);
                        stringDrinks.Append(", \n");
                    }
                    OrderListDTO appetizers = new OrderListDTO()
                    {
                        OrderItemId = OrderItemId++,
                        OrderId = item.OrderId,
                        Table = item.Table,
                        Order = stringDrinks.ToString(),
                        TimeOfOrder = item.TimeOfOrder,
                    };
                    orders.Add(appetizers);
                }

            }
            return orders.OrderBy(x => x.TimeOfOrder).ToList();
        }

        public virtual List<int> GetOrdersIds()
        {
            List<int>  ids = Context.Orders.Where(x => x.OrderStatus != Dictionaries.OrderStatusEnum.Done).Select(x => x.Id).ToList();
            return ids;
        }

    }
}
