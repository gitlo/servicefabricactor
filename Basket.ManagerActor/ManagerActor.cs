using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using Basket.ManagerActor.Interfaces;

namespace Basket.ManagerActor
{
    [StatePersistence(StatePersistence.None)]
    internal class ManagerActor : Actor, IManagerActor
    {
        public ManagerActor(ActorService actorService, ActorId actorId) 
            : base(actorService, actorId)
        {
        }

        public async Task AddBasketAsync(BasketItem basketItem, CancellationToken cancellationToken)
        {
            await this.StateManager.AddOrUpdateStateAsync("basket", basketItem, (key, value) => basketItem, cancellationToken);
        }

        public async Task<Interfaces.BasketItem> GetBasketAsync(CancellationToken cancellationToken)
        {
            return await this.StateManager.GetStateAsync<BasketItem>("basket", cancellationToken);
        }
    }
}


