using Nackowski.Models.API_ViewModels;
using Nackowski.Models.Charts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.BusinessLogic
{
    public class ManageCharts
    {
        //skapar upp klasser för användning i charts.
        //Gör beräkningar för charts.

        private string[] MonthNames()
        {
            return new string[] { "Januari", "Februari", "Mars", "April", "Maj", "Juni", "Juli", "Augusti", "September", "Oktober", "November", "December" };
        }

        private Dictionary<int, string> Dictionary()
        {
            Dictionary<int, string> priceGroupNames = new Dictionary<int, string>() { {0,"Ej Sålda"}, {1, "1-5%" }, {2, "5-15%" },
                                                                                  { 3, "15-30%" }, { 4, "30-60%" }, { 5, "60-100%" }};
            return priceGroupNames;
        }

        private string DateTimeMonthName(DateTime date)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
        }

        public List<Month> StackedDiagram(List<AuktionVM> auktions, int startMonth = 0, int amount = 12)
        {
            List<Month> months = CreateMonths(amount, startMonth);

            var vm = PutAuktionInMonthGroup(auktions, months);

            return vm;
        }

        public List<Month> UserStackedDiagram(List<AuktionVM> auktions, int startMonth = 0, int amount = 12)
        {
            List<Month> months = CreateMonths(amount, startMonth);

            var vm = PutAuktionInMonthGroup(auktions, months);

            return vm;
        }

        private List<Month> PutAuktionInMonthGroup(List<AuktionVM> auktions, List<Month> months)
        {
            List<Month> groups = months;

            foreach(var auktion in auktions)
            {
                var month = DateTimeMonthName(auktion.SlutDatum).ToLower();

                foreach (var group in groups)
                {
                    if (group.MonthName.ToLower() == month)
                    {
                        //kolla vilken prisgrupp auktionen hör till
                        int groupID = GetPriceGroup(auktion);

                        //lägg till +1 i den prisgruppen
                        group.PriceGroups.ForEach(p => {
                            if (p.ID == groupID)
                            {
                                p.SoldProducts += 1;
                            }
                        });
                    }
                }                
            }
            return groups;
        }

        //Flytta över till Calculator
        private int GetPriceGroup(AuktionVM auktion)
        {
            int startPrice = auktion.Utropspris;
            float highestBid = 0;

            if(auktion.Bud.Count == 0)
            {
                return 0;                
            }
            else if (auktion.Bud.Max(b => b.Summa) < startPrice)
            {
                return 0;
            }

            if (auktion.Bud.Count != 0)
            {
                highestBid = auktion.Bud.Max(b => b.Summa);
            }
            
            float rise = (highestBid/startPrice - 1) * 100;

            if (rise < 6)
            {
                return 1;
            }
            else if (rise < 15)
            {
                return 2;
            }
            else if (rise < 30)
            {
                return 3;
            }
            else if (rise < 60)
            {
                return 4;
            }
            else if (rise < 100)
            {
                return 5;
            }
            return 0;
        }

        private List<Month> CreateMonths(int amount, int startMonth)
        {
            List<Month> months = new List<Month>();
            string[] monthNames = MonthNames();

            if(amount == 12)
            {
                foreach(var name in monthNames) {
                    months.Add(CreateMonth(name)); 
                }
            }
            else
            {
                for(int i = startMonth; i < (startMonth + 3); i++)
                {
                    months.Add(CreateMonth(monthNames[i]));
                }
            }

            return months;
        }

        public Month CreateMonth(string name)
        {
            var priceGroupDictionary = Dictionary();

            var month = new Month()
            {
                MonthName = name,
                PriceGroups = new List<PriceGroup>()
            };

            foreach (var d in priceGroupDictionary)
            {
                PriceGroup p = new PriceGroup()
                {
                    GroupName = d.Value,
                    ID = d.Key,
                    SoldProducts = 0
                };

                month.PriceGroups.Add(p);
            }
            return month;
        }
    }
}
