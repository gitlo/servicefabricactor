using System.Threading.Tasks;
using Basket.Model;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Basket.Model
{
    public interface IManagerService : IService
    {
        Task AddBasket(int id, BasketItem basketItem);
        Task<Model.Basket> GetBasket(int id);
    }
}
