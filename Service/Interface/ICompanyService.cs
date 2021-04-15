using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface ICompanyService
    {
        public Company GetCompany(int id);
        public IEnumerable<Company> GetCompanys();
        public Company UpdateCompany(Company worker);
        public void DeleteCompany(Company worker);

        public Company CreateCompany(Company company);
    }
}
