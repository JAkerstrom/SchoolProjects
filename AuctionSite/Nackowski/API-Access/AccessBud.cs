using Nackowski.Models.APIModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;

namespace Nackowski.API_Access
{
    public class AccessBud
    {
        private static HttpClient client;

        public AccessBud()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://nackowskis.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Bud Get(string ID)
        {
            Bud model = new Bud();
            string searchString = @"api/bud/1370/" + ID;

            HttpResponseMessage response = client.GetAsync(searchString).Result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Bud));
            System.IO.Stream responseStream = response.Content.ReadAsStreamAsync().Result;

            model = (Bud)serializer.ReadObject(responseStream);
            return model;
        }

        public List<Bud> GetAll(int auktionID)
        {
            List<Bud> model = new List<Bud>();
            string searchString = @"api/bud/1370/" + auktionID;

            HttpResponseMessage response = client.GetAsync(searchString).Result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Bud>));
            System.IO.Stream responseStream = response.Content.ReadAsStreamAsync().Result;

            model = (List<Bud>)serializer.ReadObject(responseStream);
            return model;
        }

        public async Task<Bud> Post(Bud bud)
        {
            var myContent = JsonConvert.SerializeObject(bud);
            var cont = new StringContent(myContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/bud/", cont);

            return await response.Content.ReadAsAsync<Bud>();
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                @"api/bud/1370/{id}");
            return response.StatusCode;
        }
    }
}
