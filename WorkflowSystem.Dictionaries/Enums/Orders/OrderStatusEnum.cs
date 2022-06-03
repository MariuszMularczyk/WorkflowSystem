using System.ComponentModel.DataAnnotations;
using WorkflowSystem.Resources.Order;

namespace WorkflowSystem.Dictionaries
{
    public enum OrderStatusEnum : int
    {
        [Display(ResourceType = typeof(OrderStatusResource), Name = "OrderStatus_Awaiting")]
        Awaiting = 1,
        [Display(ResourceType = typeof(OrderStatusResource), Name = "OrderStatus_Done")]
        Done = 2
    }
}
