using System.Threading.Tasks;

namespace Basket.Model
{
    public interface IBasketRepository
    {
        Task AddBasket(int id, BasketItem basketItem);
        Task<Basket> GetBasket(int id);
    }
}
