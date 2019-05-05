using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalROrderSystem.Repository
{
    public class OrderRepository
    {
        private MenuRepository _menuRepository;

        public OrderRepository(MenuRepository menuRepo)
        {
            Orders = new List<Order>();
            _menuRepository = menuRepo;          
        }


        public List<Order> Orders { get; set; }


        public Order AddOrder(string name, string phonenumber, int id, string callerId)
        {
            var menuItem = _menuRepository.Get(id);

            Order order = new Order
            {
                Item = menuItem,
                PhoneNumber = phonenumber,
                Name = name,
                status = 0
            };
            
            if(Orders.Any(o => o.OrderId == order.OrderId)) //den lägger in dubletter annars, vet ej varför
                return null;              
            
            Orders.Add(order);
            return order;
        }

        public Order AddOrder(string name, string phonenumber, int itemId)
        {
            var menuItem = _menuRepository.Get(itemId);

            Order order = new Order
            {
                Item = menuItem,
                PhoneNumber = phonenumber,
                Name = name
            };

            if (Orders.Any(o => o.OrderId == order.OrderId)) //den lägger in dubletter annars, vet ej varför
                return null;

            Orders.Add(order);
            return order;
        }
    }
}
