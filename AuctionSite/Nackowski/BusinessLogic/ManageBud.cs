using Nackowski.API_Access;
using Nackowski.Data;
using Nackowski.Models.API_ViewModels;
using Nackowski.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nackowski.BusinessLogic
{
    public class ManageBud
    {
        //Postar bud till API-access lagret, hämtar bud för specifik auktion.
        private AccessBud _accessBud;

        public ManageBud(AccessBud ab)
        {
            _accessBud = ab;
        }

        public async Task<Bud> PostBud(BudVM vm)
        {
            Bud newBud = new Bud()
            {
                Summa = vm.Summa,
                AuktionID = vm.AuktionID,
                Budgivare = vm.Budgivare
            };

            Bud posted = await _accessBud.Post(newBud);           
            
            return null;
        }

        public List<BudVM> GetBudForAuktion(int auktionID)
        {
            List<Bud> list = new List<Bud>();

            list = _accessBud.GetAll(auktionID);

            List<BudVM> vm = new List<BudVM>();

            foreach (var bud in list)
            {
                BudVM newItem = new BudVM()
                {
                    BudID = bud.BudID,
                    AuktionID = bud.AuktionID,
                    Budgivare = bud.Budgivare,
                    Summa = bud.Summa
                };

                vm.Add(newItem);
            }
            return vm;
        }
    }
}
