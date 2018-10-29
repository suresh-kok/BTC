using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Travel_Request_System.Models;


namespace Travel_Request_System.Controllers
{
    public class EmployeeController : Controller
    {   public ActionResult Index()
        {

            IEnumerable<Employee> employees = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:15155/api/Travel/");
                var responseTask = client.GetAsync("GetEmployee");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var responseAsString = result.Content.ReadAsStringAsync();
                    var responseAsConcreteType = JsonConvert.DeserializeObject<IList<Employee>>(responseAsString.ToString());

                    //var readTask = result.Content.ReadAsAsync<IList<Employee>>();
                    //readTask.Wait();

                    employees = responseAsConcreteType.ToList();
                }
                else //web api sent error response 
                {
                    //log response status here..

                    employees = Enumerable.Empty<Employee>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            Employee employee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:15155/api/Travel/");
                var responseTask = client.GetAsync("GetEmployee/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var responseAsString = result.Content.ReadAsStringAsync();
                    var responseAsConcreteType = JsonConvert.DeserializeObject<Employee>(responseAsString.ToString());
                    employee = responseAsConcreteType;
                }
                else
                {
                    employee = null;
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:15155/api/Travel/");

            //    string json = JsonConvert.SerializeObject(dicti, Formatting.Indented);
            //    var httpContent = new HttpStringContent(json, System.Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");

            //    var postTask = client.PostAsync("SaveEmployee", json);
            //    postTask.Wait();

            //    var result = postTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        return RedirectToAction("Index");
            //    }
            //}

            //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
    }
}