using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TravelMVC.Models;

namespace TravelMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: UserMVC
        Uri baseAddress = new Uri("https://localhost:44391/api");
        HttpClient client;
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public ActionResult Index()
        {
            List<User> l = new List<User>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/User").Result;
            if (response.IsSuccessStatusCode)
            {
                String Data = response.Content.ReadAsStringAsync().Result;
                l = JsonConvert.DeserializeObject<List<User>>(Data);
            }
            return View(l);
        }
        public ActionResult Login()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            List<User> l = new List<User>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user").Result;
            if (response.IsSuccessStatusCode)
            {
                String Data = response.Content.ReadAsStringAsync().Result;
                l = JsonConvert.DeserializeObject<List<User>>(Data);
            }
            string Email = Request["Email"].ToString();
            string password = Request["password"].ToString();
            var found = l.Find(x => x.Email == Email);
            if (found != null)
            {
                if (found.Password == password)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Msg = "Incorrect password";
                }
            }
            else
            {
                ViewBag.Msg = "User not Found";
            }


            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(baseAddress + "/user", Content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

          public ActionResult Edit(int id)
         {
             User l = new User();
             HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user/" + id).Result;
             if (response.IsSuccessStatusCode)
             {
                 String Data = response.Content.ReadAsStringAsync().Result;
                l = JsonConvert.DeserializeObject<User>(Data);
            }
             return View(l);
         }
         [HttpPost]
         public ActionResult Edit(User model)
         {
             string data = JsonConvert.SerializeObject(model);
             StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
             HttpResponseMessage response = client.PutAsync(baseAddress + "/user/" + model.UserId, Content).Result;
             if (response.IsSuccessStatusCode)
             {
                 return RedirectToAction("Index");
             }
             return View();
         }
         public ActionResult Delete(int id)
         {
             User l = new User();
             HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user/" + id).Result;
             if (response.IsSuccessStatusCode)
             {
                 String Data = response.Content.ReadAsStringAsync().Result;
                 l = JsonConvert.DeserializeObject<User>(Data);
               
               
                 }
             return View(l);
         }
        /* [HttpPost]
         public ActionResult Delete(User model)
         {
             string data = JsonConvert.SerializeObject(model);
             StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
             HttpResponseMessage response = client.DeleteAsync(data).Result;


             if (response.IsSuccessStatusCode)
             {
                 return RedirectToAction("Index");
             }
             return View();
         }*/
       public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User u)
        {
            List<User> l = new List<User>();
            HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + "/user").Result;
            if (response1.IsSuccessStatusCode)
            {
                String Data = response1.Content.ReadAsStringAsync().Result;
                l = JsonConvert.DeserializeObject<List<User>>(Data);
            }
            u.UserId = l.Count + 1;
            u.FirstName = Request["FirstName"].ToString();
           
            u.LastName = Request["LastName"].ToString();
            u.Email = Request["Email"].ToString();
            var found = l.Find(x => x.Email == u.Email);
            if (found != null)
            {
                TempData["msg"] = "User Already Register with this Email";
                return RedirectToAction("Register");
            }
            else
            {
                u.Password = Request["Password"].ToString();
                TempData["Cpassword"] = Request["ConformPassword"].ToString();
               

              
                string data = JsonConvert.SerializeObject(u);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/user", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    if (u.Password == TempData["Cpassword"].ToString())
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["msg"] = "Password and confirm  password not Matched Please check password and confirm password while Entering";
                    }
                }
                return RedirectToAction("Register");
            }

        }
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(FormCollection collection)
        {
            List<User> l = new List<User>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user").Result;
            if (response.IsSuccessStatusCode)
            {
                String Data = response.Content.ReadAsStringAsync().Result;
                l = JsonConvert.DeserializeObject<List<User>>(Data);
            }
            string Email = Request["Email"].ToString();
            string Password = Request["Password"].ToString();
            var found = l.Find(x => x.Email == Email);
            if (found != null)
            {
                if (found.Password == Password)
                {
                    TempData["userid"] = found.UserId;
                    TempData["Email"] = found.Email;
                    // loginUser = found;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.Msg = "Incorrect password";
                }
            }
            else
            {
                ViewBag.Msg = "User not Found";
            }
            return View();
        }
    }


}