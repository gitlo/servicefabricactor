using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basket.ManagerActor.Interfaces;
using Basket.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;

namespace Basket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IManagerService managerService;

        public BasketController()
        {
            this.managerService = new ServiceProxyFactory(c =>
            new FabricTransportServiceRemotingClientFactory())
                .CreateServiceProxy<IManagerService>(new Uri("fabric:/Basket/Basket.Manager"), new ServicePartitionKey(0));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Model.Basket>> Get(int id)
        {
            return await this.managerService.GetBasket(id);
        }

        [HttpPost]
        public async Task Post([FromBody] Model.Basket value)
        {
            var name = value.BasketItems[0].Name;
            await managerService.AddBasket(value.Id, new Model.BasketItem{Name=name });
        }
    }
}


