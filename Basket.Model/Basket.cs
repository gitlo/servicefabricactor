using System.Collections.Generic;

namespace Basket.Model
{
    public class Basket
    {
        public Basket()
        {
            BasketItems = new List<BasketItem>();
        }
        public int Id { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
