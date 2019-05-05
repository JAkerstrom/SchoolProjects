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

namespace Nackowski.API_Access
{
    public class AccessAuktion
    {
        public const string API_ADRESS = "";
        private static HttpClient client;

        public AccessAuktion()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(API_ADRESS);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Post(Auktion auktion)
        {

            var myContent = JsonConvert.SerializeObject(auktion);
            var cont = new StringContent(myContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(
                "api/Auktion/", cont);
        }

        public async Task<Auktion> Put(Auktion auktion)
        {
            var myContent = JsonConvert.SerializeObject(auktion);
            var cont = new StringContent(myContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(
            "api/Auktion/1370/" + auktion.AuktionID, cont);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            auktion = await response.Content.ReadAsAsync<Auktion>();
            return auktion;
        }



        public Auktion Get(string ID)
        {
            Auktion model = new Auktion();
            string searchString = @"api/auktion/1370/" + ID;

            HttpResponseMessage response = client.GetAsync(searchString).Result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Auktion));
            System.IO.Stream responseStream = response.Content.ReadAsStreamAsync().Result;

            model = (Auktion)serializer.ReadObject(responseStream);
            return model;
        }

        public List<Auktion> GetAll()
        {
            List<Auktion> auktions = new List<Auktion>();
            string searchString = @"api/auktion/1370/";

            HttpResponseMessage response = client.GetAsync(searchString).Result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Auktion>));
            System.IO.Stream responseStream = response.Content.ReadAsStreamAsync().Result;

            auktions = (List<Auktion>)serializer.ReadObject(responseStream);

            return auktions;
        }

        public List<Auktion> GetOpenAuktions()
        {
            List<Auktion> auktions = GetAll();

            var dateAndTime = DateTime.Now;
            
            return auktions.Where(a => DateTime.Parse(a.SlutDatum) > dateAndTime).ToList();
            
        }

        public List<Auktion> GetClosedAuktions()
        {
            List<Auktion> auktions = GetAll();

            var dateAndTime = DateTime.Now;

            return auktions.Where(a => DateTime.Parse(a.SlutDatum) < dateAndTime).ToList();
        }

        public List<Auktion> GetUserClosedAuktions(string userName)
        {
            List<Auktion> auktions = GetAll().Where(a => a.SkapadAv == userName.ToString()).ToList();

            var dateAndTime = DateTime.Now;

            return auktions.Where(a => DateTime.Parse(a.SlutDatum) < dateAndTime).ToList();
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                @"api/auktion/1370/" + id);
            return response.StatusCode;
        }
    }
}
