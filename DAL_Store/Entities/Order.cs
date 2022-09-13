using System.Collections.Generic;

namespace Store.Entities
{
   public class Order
    {
        public int Id { get; set; }
        public List<Goods> Product { get; set; } = new List<Goods>();
        public string Customer { get; set; }
        public StatusOfOrder Status { get; set; }
        
        public override string ToString()
        {
            return $"Id: {Id} \n" +
                   $"Product: {Product} \n" +
                   $"Status of order: {Status} \n" +
                   $"Customer: {Customer} \n";
        }
    }
}
