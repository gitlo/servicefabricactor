using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;

[assembly: FabricTransportActorRemotingProvider(RemotingListenerVersion = RemotingListenerVersion.V2_1, RemotingClientVersion = RemotingClientVersion.V2_1)]
namespace Basket.ManagerActor.Interfaces
{
    public interface IManagerActor : IActor
    {
        Task<BasketItem> GetBasketAsync(CancellationToken cancellationToken);
        Task AddBasketAsync(BasketItem basketItem, CancellationToken cancellationToken);
    }

    public class Basket
    {
        public Basket()
        {
            BasketItems = new List<BasketItem>();
        }
        public int Id { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }

    public class BasketItem
    {
        public string Name { get; set; }
    }
}
