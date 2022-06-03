
using Autofac;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WorkflowSystem.WebAPI
{

    public class WaiterHub : Hub
    {
        public async Task GetWaiter(object table)
        {
            await Clients.All.SendAsync("SendWaiter", table);
        }
    }
}
