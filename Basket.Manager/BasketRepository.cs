using Basket.Model;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Manager
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IReliableStateManager stateManager;

        public BasketRepository(IReliableStateManager stateManager)
        {
            this.stateManager = stateManager;
        }
        public async Task AddBasket(int id, BasketItem basketItem)
        {
            var baskets = await this.stateManager.GetOrAddAsync<IReliableDictionary<int, Model.Basket>>("shoppingbaskets");
            var theBasket = await this.GetBasket(id);

            theBasket.BasketItems.Add(basketItem);

            using (var tx = this.stateManager.CreateTransaction())
            {
                await baskets.AddOrUpdateAsync(tx, id, theBasket, (key, value) => theBasket);
                await tx.CommitAsync();
            }
        }

        public async Task<Model.Basket> GetBasket(int id)
        {
            var result = new Model.Basket();
            var baskets = await this.stateManager.GetOrAddAsync<IReliableDictionary<int, Model.Basket>>("shoppingbaskets");

            var newBasket = new Model.Basket
            {
                Id = id
            };

            using (var tx = this.stateManager.CreateTransaction())
            {
                result = await baskets.GetOrAddAsync(tx, id, newBasket);
                await tx.CommitAsync();
            }

            return result;
        }
    }
}
