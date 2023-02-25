using Project_1.Models;
using Project_1.Models.ViewModels;
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
            client.BaseAddress = new Uri("https://localhost:44379/api/");

        }
        // GET: Department/List
        public ActionResult List()
        {
            //objective: communicate with our animal data api to retrive a list of Departments
            //curl https://localhost:44379/api/DepartmentData/ListDepartments

        

            string url = "DepartmentData/ListDepartments";
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

            DetailsDepartments ViewModel = new DetailsDepartments();

         

            string url = "DepartmentData/FindDepartments/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The respoanse code is : ");
            Debug.WriteLine(response.StatusCode);

            DepartmentsDto selectedDepartment = response.Content.ReadAsAsync<DepartmentsDto>().Result;
            Debug.WriteLine("Department received : ");
            Debug.WriteLine(selectedDepartment.DepartmentName);

            ViewModel.SelectedDepartments= selectedDepartment;
            //Showcase information about Employee related to this Department
            //Send a request to gather information about Employees related to a particular DepartmentID
            url = "EmployeeData/ListEmployeesForDepartment/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<EmployeeDto> RelatedEmployees = response.Content.ReadAsAsync<IEnumerable<EmployeeDto>>().Result;
            ViewModel.RelatedEmployees = RelatedEmployees;


            return View(ViewModel);
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
            string url = "DepartmentData/AddDepartments";
            
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
            string url = "DepartmentData/FindDepartments/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DepartmentsDto selectedDepartment = response.Content.ReadAsAsync<DepartmentsDto>().Result;
            return View(selectedDepartment);
        }

        // POST: Department/Update/5
        [HttpPost]
        public ActionResult Update(int id, Departments departments)
        {
            string url = "DepartmentData/UpdateDepartments/" + id;
            string jsonpayload = jss.Serialize(departments);
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

        // GET: Department/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "DepartmentData/FindDepartments/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DepartmentsDto selectedDepartment = response.Content.ReadAsAsync<DepartmentsDto>().Result;
            return View(selectedDepartment);
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Departments departments)
        {
            string url = "DepartmentData/DeleteDepartments/" + id;
            string jsonplayload = jss.Serialize(departments);
            HttpContent content = new StringContent(jsonplayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url,content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else 
            { 
                return RedirectToAction("Error"); 
            }
        }
    }
}
