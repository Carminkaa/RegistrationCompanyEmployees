using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace RegistrationCompanyEmployees.Controllers
{
    public class CompanyController : Controller
    {

        protected ICompanyService service;
        public CompanyController(ICompanyService companyService) {
            this.service = companyService;
        }

        public IActionResult Index()
        {
            var companys = service.GetCompanys();
            return View(service.GetCompanys());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            service.CreateCompany(company);
            return Redirect("/Company");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            service.DeleteCompany(service.GetCompany(id));
            return Redirect("/Company");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(service.GetCompany(id));
        }
        [HttpPost]
        public IActionResult Edit(Company company)
        {
            service.UpdateCompany(company);
            return Redirect("/Company");
        }
    }
}
