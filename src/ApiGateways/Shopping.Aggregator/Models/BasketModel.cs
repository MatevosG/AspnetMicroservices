using System.Collections.Generic;

namespace Shopping.Aggregator.Models
{
    public class BasketModel
    {
        public string UserName { get; set; }
        public List<BasketItemExtendeModel> Items { get; set; } = new List<BasketItemExtendeModel>();
        public decimal TotalPrice { get; set; }
    }
}
