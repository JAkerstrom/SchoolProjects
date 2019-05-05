using Microsoft.Extensions.Caching.Memory;
using Nackowski.API_Access;
using Nackowski.Caching;
using Nackowski.Models.API_ViewModels;
using Nackowski.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.BusinessLogic
{
    //Hämtar auktioner och dess bud från API-Access lagret, och omvandlar dessa till VM.
    //Sökfunktion, sorteringsfunktion.

    public class ManageAuktion
    {
        private AccessAuktion _accessAuktion;
        private ManageBud _manageBud;
        private IMemoryCache _cache;

        public ManageAuktion(AccessAuktion aa, ManageBud mb, IMemoryCache memoryCache)
        {
            _accessAuktion = aa;
            _manageBud = mb;
            _cache = memoryCache;
        }

        private AuktionVM CreateVM(string id) {

            Auktion auktion = _accessAuktion.Get(id);

            List<BudVM> budVMs = _manageBud.GetBudForAuktion(auktion.AuktionID);

            AuktionVM vm = new AuktionVM()
            {
                AuktionID = auktion.AuktionID,
                Titel = auktion.Titel,
                Beskrivning = auktion.Beskrivning,
                Utropspris = auktion.Utropspris,
                StartDatum = DateTime.Parse(auktion.StartDatum),
                SlutDatum = DateTime.Parse(auktion.SlutDatum),
                SkapadAv = auktion.SkapadAv,
                Bud = budVMs
            };
            return vm;
        }

        private void SetAuktionsCache(List<AuktionVM> auktions){
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));
            _cache.Set(CacheKeys.Auktions, auktions, cacheEntryOptions);
        }

        public AuktionVM EmptyVM()
        {
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;

           
            AuktionVM vm = new AuktionVM()
            {
                StartDatum = dateAndTime,
                SlutDatum = dateAndTime.AddDays(7)
            };

            return vm;
        }

        public AuktionVM GetAuktion(string id)
        {
            AuktionVM vm = CreateVM(id);

            return vm;
        }

        public List<AuktionVM> GetAuktioner()
        {
            List<Auktion> auktioner = _accessAuktion.GetAll();

            List<AuktionVM> vms = new List<AuktionVM>();

            foreach (var auktion in auktioner)
            {
                AuktionVM vm = CreateVM(auktion.AuktionID.ToString());

                vms.Add(vm);
            }

            return vms;
        }


        public List<AuktionVM> GetOpenAuktions()
        {
            List<Auktion> auktioner = _accessAuktion.GetOpenAuktions();

            List<AuktionVM> vms = new List<AuktionVM>();

            foreach (var auktion in auktioner)
            {
                AuktionVM vm = CreateVM(auktion.AuktionID.ToString());

                vms.Add(vm);
            }

            return vms;
        }

        public List<AuktionVM> GetClosedAuktions()
        {
            List<Auktion> auktioner = _accessAuktion.GetClosedAuktions();

            List<AuktionVM> vms = new List<AuktionVM>();

            foreach (var auktion in auktioner)
            {
                AuktionVM vm = CreateVM(auktion.AuktionID.ToString());

                vms.Add(vm);
            }

            return vms;
        }

        public List<AuktionVM> GetUserClosedAuktions(string userName)
        {
            List<Auktion> auktioner = _accessAuktion.GetUserClosedAuktions(userName);

            List<AuktionVM> vms = new List<AuktionVM>();

            foreach (var auktion in auktioner)
            {
                AuktionVM vm = CreateVM(auktion.AuktionID.ToString());

                vms.Add(vm);
            }

            return vms;

        }


        public List<AuktionVM> CacheGetSetOpenAuktions()
        {
            List<AuktionVM> auktions;

            if (!_cache.TryGetValue(CacheKeys.Auktions, out auktions))
            {
                auktions = GetOpenAuktions();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

                _cache.Set(CacheKeys.Entry, auktions, cacheEntryOptions);
            }

            return auktions;
        }

        public List<AuktionVM> Search(string searchString)
        {
            List<AuktionVM> result = new List<AuktionVM>();
            TitleSearch(searchString.ToLower()).ForEach(r => result.Add(r));
            DescriptionSearch(searchString.ToLower()).ForEach(r => result.Add(r));
            SetAuktionsCache(result);

            return result;
        }

        public List<AuktionVM> TitleSearch(string searchString)
        {
            return GetAuktioner().Where(a => a.Titel.ToLower().Contains(searchString)).ToList();
        }

        public List<AuktionVM> DescriptionSearch(string searchString)
        {
            return GetAuktioner().Where(a => a.Beskrivning.ToLower().Contains(searchString)).ToList();
        }

        public List<AuktionVM> Sort(string method)
        {
            List<AuktionVM> auktioner = new List<AuktionVM>();

            auktioner = CacheGetSetOpenAuktions();        

            List<AuktionVM> sorted = new List<AuktionVM>();

            switch (method)
            {
                case "endDate":
                    sorted = auktioner.OrderBy(a => a.SlutDatum).ToList();
                    break;
                case "startDate":
                    sorted = auktioner.OrderBy(a => a.StartDatum).ToList();
                    break;
                case "price":
                    sorted = auktioner.OrderBy(a => a.Utropspris).ToList();
                    break;
                case "isOpen":
                    var today = DateTime.Now;
                    sorted = auktioner.Where(a => a.SlutDatum > today && a.StartDatum < today).ToList();
                    break;
                case "all":
                    sorted = auktioner.OrderBy(a => a.Titel).ToList();
                    break;
            }
            return sorted;
        }

        public async Task PutAuktion(AuktionVM vm)
        {
            Auktion changedAuktion = new Auktion()
            {
                AuktionID = vm.AuktionID,
                Titel = vm.Titel,
                Beskrivning = vm.Beskrivning,
                Utropspris = vm.Utropspris,
                Gruppkod = 1370,
                SkapadAv = vm.SkapadAv,
                StartDatum = vm.StartDatum.ToString(),
                SlutDatum = vm.SlutDatum.ToString()
            };
            await _accessAuktion.Put(changedAuktion);
        }

        public async Task PostAuktion(AuktionVM vm)
        {
            Auktion auktion = new Auktion() {
                Titel = vm.Titel,
                Utropspris = vm.Utropspris,
                SkapadAv = vm.SkapadAv,
                Beskrivning = vm.Beskrivning,
                Gruppkod = 1370,
                StartDatum = vm.StartDatum.ToString(),
                SlutDatum = vm.SlutDatum.ToString()
            };

            await _accessAuktion.Post(auktion);
        }

        public async Task<string> DeleteAuktion(string id)
        {
            
            if (GetAuktion(id).Bud.Count() == 0)
            {
                await _accessAuktion.Delete(id);

                return "auktionen har raderats";
            }

            return "Kan inte radera en auktion som har bud";      
        }
    }
}
