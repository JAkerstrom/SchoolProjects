using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalROrderSystem.Repository
{
    public class MenuItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AmountInStock { get; set; }

        public MenuItem(int id, string name, string description, 
            decimal price, int amountInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            AmountInStock = amountInStock;
        }

        public MenuItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public bool InStock()
        {
            if (AmountInStock > 0) return true;
            else return false;
        }
    }
}
