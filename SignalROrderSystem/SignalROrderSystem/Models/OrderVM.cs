using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalROrderSystem.Models
{
    public class OrderVM
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public int ItemId { get; set; }
        public int Status { get; set; }
    }
}
