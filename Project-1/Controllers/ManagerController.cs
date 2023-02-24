using Project_1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Project_1.Controllers
{
    public class ManagerController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();
        static ManagerController()
        { 
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44379/api/ManagerData/");
        }
        // GET: Manager/List
        public ActionResult List()
        {
            //objective: communicate with our Manager data api to retrive a list of Manager
            //curl https://localhost:44379/api/ManagerData/ListManagers
           
            string url = "ListManagers";
            HttpResponseMessage response = client.GetAsync(url).Result;
            
            //Debug.WriteLine("The response code is: ");
           // Debug.WriteLine(response.StatusCode);

            IEnumerable<ManagerDto> Managers = response.Content.ReadAsAsync<IEnumerable<ManagerDto>>().Result;
            //Debug.WriteLine("Number of Manager received : ");
            //Debug.WriteLine(managers.Count());
            
            return View(Managers);
        }

        // GET: Manager/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our Manager data api to retrive one Manager
            //curl https://localhost:44379/api/ManagerData/FindManager/{id}
      
            string url = "FindManager/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is: ");
            //Debug.WriteLine(response.StatusCode);

            ManagerDto selectedManager = response.Content.ReadAsAsync<ManagerDto>().Result;
            //Debug.WriteLine("Manager received : ");
           //Debug.WriteLine(selectedManager.ManagerName);
           

            return View(selectedManager);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Manager/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Manager/Create
        [HttpPost]
        public ActionResult Create(Manager manager)
        {
            Debug.WriteLine("the json payload is : ");
            Debug.WriteLine(manager.ManagerName);

            //objective: Add a new Manager into our system using the API
            //curl -H "Content-Type:application/json" -d @manager.json https://localhost:44379/api/ManagerData/AddManager
            
            string url = "AddManager";

            string jsonplayload = jss.Serialize(manager);

            Debug.WriteLine(jsonplayload);

            HttpContent content = new StringContent(jsonplayload);
            content.Headers.ContentType.MediaType= "application/json";

            HttpResponseMessage response =  client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
          
        }

        // GET: Manager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
