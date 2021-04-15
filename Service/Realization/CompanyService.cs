using Common.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Realization
{
    public class CompanyService:ICompanyService
    {
        protected readonly IRepository<Company> repository;

        public CompanyService(IRepository<Company> repository) {
            this.repository = repository;
        }

        public Company CreateCompany(Company company)
        {
            return repository.Create(company);
        }

        public void DeleteCompany(Company worker)
        {
            repository.Delete(worker);
        }

        public Company GetCompany(int id)
        {
            return repository.Read(id);
        }

        public IEnumerable<Company> GetCompanys()
        {
            return repository.ReadAll();
        }

        public Company UpdateCompany(Company worker)
        {
            return repository.Update(worker);
        }
    }
}
