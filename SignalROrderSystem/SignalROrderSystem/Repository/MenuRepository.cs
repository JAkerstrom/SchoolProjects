using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalROrderSystem.Repository
{
    public class MenuRepository
    {
        private List<MenuItem> menu;

        public MenuRepository()
        {
            CreateMenu();
        }

        public List<MenuItem> Menu { get{
                return menu;
            }
        }

        public MenuItem Get(int id)
        {
            var menuItem = menu.Where(item => item.Id == id).SingleOrDefault();
            menuItem.AmountInStock -= 1;
            return menuItem;
        }

        private void CreateMenu()
        {
            menu = new List<MenuItem>()
            {
                new MenuItem(1, "Carbonara", "Very good pasta dish", 45, 0),
                new MenuItem(2, "Rågsmörgås", "Fantastic", 30, 5),
                new MenuItem(3, "Ostfralla", "Vitt bröd, sallad, smör, ost", 25, 5),
                new MenuItem(4, "Morotskaka", "Gjord av laktosfri mjölk", 60, 10),
                new MenuItem(5, "Vaniljsemla", "Gjord på laktosfri grädde", 40, 3),
                new MenuItem(6, "Köttbullar", "Serveras med lingonsylt och grädde", 55, 7),
                new MenuItem(7, "Kaffe", "Med mjölk", 20, 15)
            };

        }
    }
}
