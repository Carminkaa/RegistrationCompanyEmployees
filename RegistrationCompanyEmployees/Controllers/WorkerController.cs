using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Interface;
using Service.Interface;

namespace RegistrationCompanyEmployees.Controllers
{
    public class WorkerController : Controller
    {
        protected IUserService service;
        protected ICompanyService companyService;
        public WorkerController(IUserService service,ICompanyService companyService) {
            this.service = service;
            this.companyService = companyService;
        }
        public IActionResult Index()
        {
            return View(service.GetWorkers());
        }

        public IActionResult Create()
        {
            
            List<SelectListItem> listhosp = new List<SelectListItem>();

            foreach (var item in companyService.GetCompanys())
            {
                listhosp.Add(new SelectListItem
                {
                    Text = item.Title,
                    Value = item.ID.ToString()
                });
            }
            ViewData["Companys"] = listhosp;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Worker worker)
        {
          
            service.CreateWorker(worker);
            return Redirect("/Worker");
        }

        public IActionResult Edit(int id)
        {

            List<SelectListItem> listhosp = new List<SelectListItem>();

            foreach (var item in companyService.GetCompanys())
            {
                listhosp.Add(new SelectListItem
                {
                    Text = item.Title,
                    Value = item.ID.ToString()
                });
            }
            ViewData["Companys"] = listhosp;
            return View(service.GetWorker(id));
        }
        [HttpPost]
        public IActionResult Edit(Worker worker)
        {

            service.UpdateWorker(worker);
            return Redirect("/Worker");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            service.DeleteWorker(service.GetWorker(id));
            return Redirect("/Worker");
        }
    }
}
