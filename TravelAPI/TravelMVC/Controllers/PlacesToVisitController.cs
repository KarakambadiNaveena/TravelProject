using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TravelMVC.Models;

namespace TravelMVC.Controllers
{
    public class PlacesToVisitController : Controller
    {
        // GET: PlacesToVisit
        Uri baseAddress = new Uri("https://localhost:44391/api");
        HttpClient client;
        public PlacesToVisitController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public ActionResult Index()
        {
            List<PlacesToVisit> l = new List<PlacesToVisit>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/PlacesToVisit").Result;
            if (response.IsSuccessStatusCode)
            {
                String Data = response.Content.ReadAsStringAsync().Result;
                l = JsonConvert.DeserializeObject<List<PlacesToVisit>>(Data);
            }
            return View(l);
        }
    }
}