using Project_1.Models;
using Project_1.Models.ViewModels;
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
    public class EmployeeController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static EmployeeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44379/api/");
        }
        // GET: Employee/List
        public ActionResult List()
        {
            //objective: communicate with our employee data api to retrive a list of employees
            //curl https://localhost:44379/api/EmployeeData/ListEmployees

            string url = "EmployeeData/ListEmployees";
            HttpResponseMessage response = client.GetAsync(url).Result;
            
            //Debug.WriteLine("The response code is");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<EmployeeDto> Employees = response.Content.ReadAsAsync<IEnumerable<EmployeeDto>>().Result;
            //Debug.WriteLine("Number of Employees received : ");
            //Debug.WriteLine(Employees.Count());

            return View(Employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our employee data api to retrieve one employee
            //curl https://localhost:44379/api/EmployeeData/FindEmployee/{id}

            string url = "EmployeeData/FindEmployee/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is");
            //Debug.WriteLine(response.StatusCode);

            EmployeeDto selectedEmployees = response.Content.ReadAsAsync<EmployeeDto>().Result;
            //Debug.WriteLine("Employee received : ");
            //Debug.WriteLine(selectedEmployees.EmployeeName);

            

            return View(selectedEmployees);
        }
        public ActionResult Error() 
        {

            return View();
        
        
        }

        // GET: Employee/New
        public ActionResult New()
        {
            //information about all department in the system
            //GET api/DepartmentData/ListDepartments
            string url = "DepartmentData/ListDepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DepartmentsDto> DepartmentsOptions = response.Content.ReadAsAsync<IEnumerable<DepartmentsDto>>().Result;

            return View(DepartmentsOptions);
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            Debug.WriteLine("The json payload is : ");
            Debug.WriteLine(employee.EmployeeName);
            //objective: add a new Employee into our system using API
            //curl -H "Content-Type"application/json" -d @employee.json https://localhost:44379/api/EmployeeData/AddEmployee
            string url = "EmployeeData/AddEmployee";

 
            string jsonpayload = jss.Serialize(employee);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType= "application/json";

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

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateEmployee ViewModel = new UpdateEmployee();
            // the existing Employee information
            string url = "EmployeeData/FindEmployee/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            EmployeeDto selectedEmployee = response.Content.ReadAsAsync<EmployeeDto>().Result;
            ViewModel.SelectedEmployee = selectedEmployee;
            //all Department to choose from when updating this Employee
            // the existing Employee information
            url = "DepartmentData/ListDepartments/";
            response = client.GetAsync(url).Result;

            IEnumerable<DepartmentsDto> DepartmentsOption = response.Content.ReadAsAsync<IEnumerable<DepartmentsDto>>().Result;

            ViewModel.DepartmentsOptions= DepartmentsOption;

            return View(ViewModel);
        }

        // POST: Employee/Update/5
        [HttpPost]
        public ActionResult Update(int id, Employee employee)
        {

            string url = "EmployeeData/UpdateEmployee/" + id;
            string jsonpayload = jss.Serialize(employee);

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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // GET: Employee/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "EmployeeData/FindEmployee/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            EmployeeDto selectedEmployee = response.Content.ReadAsAsync<EmployeeDto>().Result;

            return View(selectedEmployee);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            string url = "EmployeeData/DeleteEmployee/" + id;

            string jsonplayload = jss.Serialize(employee);

            HttpContent content = new StringContent(jsonplayload);
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
    }
}
