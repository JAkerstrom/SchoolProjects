using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalROrderSystem.Repository;

namespace SignalROrderSystem.Hubs
{
    public class RestaurantHub : Hub
    {
        private MenuRepository _menuRepository;
        private OrderRepository _orderRepository;

        public RestaurantHub(MenuRepository menuRepo, OrderRepository orderRepo)
        {
            _menuRepository = menuRepo;
            _orderRepository = orderRepo;
        }

        public async Task AddClient(int orderid)
        {
            var callerId = Context.ConnectionId;
            await Groups.AddToGroupAsync(callerId, orderid.ToString());
        }


        public async Task NewOrderMessage(string name, string phonenumber, string itemid)
        {
            var order = _orderRepository.Orders.Where(o => 
                                            o.Name == name && 
                                            o.PhoneNumber == phonenumber && 
                                            o.Item.Id == int.Parse(itemid))
                                            .FirstOrDefault();

            await Clients.All.SendAsync("NewOrder", name, phonenumber, order.OrderId, order.Item.Name);

        }

        public async Task BeginOrder(string orderid)
        {
            _orderRepository.Orders.Where(o => o.OrderId == orderid).FirstOrDefault().status = 1;
            await Clients.Group(orderid).SendAsync("OrderProgressMessage", 1);
        }

        public async Task FinnishOrder(string orderid)
        {
            _orderRepository.Orders.Where(o => o.OrderId == orderid).FirstOrDefault().status = 2;
            await Clients.Group(orderid).SendAsync("OrderProgressMessage", 2);
        }

        public async Task PickupOrder(string orderid)
        {
            _orderRepository.Orders.Where(o => o.OrderId == orderid).FirstOrDefault().status = 3;
            //var order = _orderRepository.Orders.Where(o => o.OrderId == orderid).FirstOrDefault();
            //_orderRepository.Orders.Remove(order);
            await Clients.Group(orderid).SendAsync("OrderProgressMessage", 3);
        }

        public async Task OutOfStock(string id)
        {
            _menuRepository.Menu.Where(item => item.Id == int.Parse(id)).FirstOrDefault().AmountInStock = 0;
            await Clients.AllExcept(Context.ConnectionId).SendAsync("OutOfStock", id);
        }

        public async Task BackInStock(string id)
        {
            _menuRepository.Menu.Where(item => item.Id == int.Parse(id)).FirstOrDefault().AmountInStock += 10;
            await Clients.AllExcept(Context.ConnectionId).SendAsync("BackInStock", id);
        }

    }
}
