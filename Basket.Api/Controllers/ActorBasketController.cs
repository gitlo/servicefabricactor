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
    public class ActorBasketController : ControllerBase
    {
        public ActorBasketController()
        {

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Model.Basket>> Get(int id)
        {
            var actor = ActorProxy.Create<IManagerActor>(new ActorId(id), new Uri("fabric:/Basket/ManagerActorService"));

            var result = await actor.GetBasketAsync(CancellationToken.None);
            var bskt = new Model.Basket();
            bskt.Id = id;
            bskt.BasketItems.Add(new Model.BasketItem { Name=result.Name });
            return bskt;
        }

        [HttpPost]
        public async Task Post([FromBody] Model.Basket value)
        {
            var actor = ActorProxy.Create<IManagerActor>(new ActorId(value.Id), new Uri("fabric:/Basket/ManagerActorService"));

            var name = value.BasketItems[0].Name;
            var xx = new ManagerActor.Interfaces.BasketItem{ Name = name};
            await actor.AddBasketAsync(xx, CancellationToken.None);
        }
    }
}


