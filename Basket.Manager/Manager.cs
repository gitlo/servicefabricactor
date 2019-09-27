using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basket.Model;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Basket.Manager
{
    internal sealed class Manager : StatefulService, IManagerService
    {
        private readonly IBasketRepository _repo;

        public Manager(StatefulServiceContext context)
            : base(context)
        {
            _repo = new BasketRepository(this.StateManager);
        }

        public async Task AddBasket(int id, BasketItem basketItem)
        {
            await _repo.AddBasket(id, basketItem);
        }

        public async Task<Model.Basket> GetBasket(int id)
        {
           return  await _repo.GetBasket(id);
        }

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                new ServiceReplicaListener(c => new FabricTransportServiceRemotingListener(c, this))
            };
        }
    }
}
