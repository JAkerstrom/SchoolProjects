using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalROrderSystem.Repository;

namespace SignalROrderSystem.Models
{
    public class AddOrderModel
    {
        public MenuItem Item { get; set; }
        public OrderVM Order { get; set; }
    }
}
