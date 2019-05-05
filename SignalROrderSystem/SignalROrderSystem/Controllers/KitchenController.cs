using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalROrderSystem.Repository;

namespace SignalROrderSystem.Controllers
{
    public class KitchenController : Controller
    {
        OrderRepository _orderRepository;
        MenuRepository _menuRepository;

        public KitchenController(OrderRepository orderRepo, MenuRepository menuRepo)
        {
            _orderRepository = orderRepo;
            _menuRepository = menuRepo;
        }

        public IActionResult Index()
        {            
            return View(_orderRepository.Orders.Where(o => o.status != 3).ToList());
        }

        public IActionResult MenuEditor()
        {
            var menu = _menuRepository.Menu;
            return View(menu);
        }
    }
}