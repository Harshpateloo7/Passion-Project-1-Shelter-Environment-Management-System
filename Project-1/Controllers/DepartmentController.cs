using Project_1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Project_1.Controllers
{
    public class DepartmentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static DepartmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44379/api/DepartmentData/");

        }
        // GET: Department/List
        public ActionResult List()
        {
            //objective: communicate with our animal data api to retrive a list of Departments
            //curl https://localhost:44379/api/DepartmentData/ListDepartments

        

            string url = "ListDepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The respoanse code is : ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<DepartmentsDto> Departments = response.Content.ReadAsAsync<IEnumerable<DepartmentsDto>>().Result;
            Debug.WriteLine("Number of Department received");
            Debug.WriteLine(Departments.Count());
            
            return View(Departments);
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our animal data api to retrive a one Department
            //curl https://localhost:44379/api/DepartmentData/FindDepartments/{id}

         

            string url = "FindDepartments/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The respoanse code is : ");
            Debug.WriteLine(response.StatusCode);

            DepartmentsDto selectedDepartment = response.Content.ReadAsAsync<DepartmentsDto>().Result;
            Debug.WriteLine("Department received : ");
            Debug.WriteLine(selectedDepartment.DepartmentName);
            

            return View(selectedDepartment);
        }
        public ActionResult Error()
        {
            return View();
        }


        // GET: Department/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(Departments departments)
        {
            Debug.WriteLine(" the json payload is: ");
            //Debug.WriteLine(departments.DepartmentName);

            //objective: add a new Department into our system using API
            //curl -H "Content-Type:application/json" -d https://localhost:44379/api/DepartmentData/AddDepartments
            string url = "AddDepartments";
            
            string jsonpayload = jss.Serialize(departments);

            Debug.WriteLine(jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");

            }
          
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Department/Edit/5
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

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Department/Delete/5
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
