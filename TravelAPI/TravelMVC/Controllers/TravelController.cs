using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TravelMVC.Models;

namespace TravelMVC.Controllers
{
    public class TravelController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:44391/api");
        HttpClient client;
        public TravelController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public ActionResult Index()
        {
            List<TravelDetails> l = new List<TravelDetails>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Travel").Result;
            if (response.IsSuccessStatusCode)
            {
                String Data = response.Content.ReadAsStringAsync().Result;
                l = JsonConvert.DeserializeObject<List<TravelDetails>>(Data);
            }
            return View(l);
        }

    }
}