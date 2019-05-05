using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalROrderSystem.Repository
{
    public class Order
    {
        public string OrderId { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public MenuItem Item { get; set; }
        public int status { get; set; }

        public Order()
        {
            Random random = new Random();
            OrderId = random.Next(1, 100).ToString();
        }

        public Order(int orderId){}
    }
}
