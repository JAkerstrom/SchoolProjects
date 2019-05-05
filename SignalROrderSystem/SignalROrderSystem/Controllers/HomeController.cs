using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalROrderSystem.Hubs;
using SignalROrderSystem.Models;
using SignalROrderSystem.Repository;

namespace SignalROrderSystem.Controllers
{
    public class HomeController : Controller
    {
        private IHubContext<RestaurantHub> _hubContext;
        private MenuRepository _menuRepository;
        private OrderRepository _orderRepository;

        public HomeController(MenuRepository menuRepo, OrderRepository orderRepo, IHubContext<RestaurantHub> hubContext)
        {
            _hubContext = hubContext;
            _menuRepository = menuRepo;
            _orderRepository = orderRepo;
        }

        public IActionResult Index()
        {
            return View(_menuRepository.Menu);
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            var item = _menuRepository.Menu.Where(i => i.Id == id).FirstOrDefault();

            AddOrderModel model = new AddOrderModel() {
                Item = item,
                Order = new OrderVM()
                {
                    ItemId = item.Id
                }
            };
            return View("AddOrder", model);
        }

        [HttpPost]
        public IActionResult AddOrder(AddOrderModel vm)
        { 
            var order = _orderRepository.AddOrder(vm.Order.Name, vm.Order.PhoneNumber, vm.Order.ItemId);

            _hubContext.Clients.All.SendAsync("NewOrder", order.Name, order.PhoneNumber, order.OrderId, order.Item.Name);
            return RedirectToAction("OrderStatus", "Home", new { orderId = order.OrderId });
        }

        [HttpGet]
        public IActionResult OrderStatus(string orderId)
        {
            var model = _orderRepository.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();
            return View(model);
        }

    }
}
