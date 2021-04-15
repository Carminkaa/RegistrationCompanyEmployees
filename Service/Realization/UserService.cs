using Common.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Realization
{
    public class UserService : IUserService
    {
        protected IRepository<Worker> workerRep;
        protected IRepository<Company> companyRep;
        public UserService(IRepository<Worker> workers, IRepository<Company> companys) {
            this.workerRep = workers;
            this.companyRep = companys;
        }

        public Worker CreateWorker(Worker worker)
        {
            return workerRep.Create(worker);
        }

        public void DeleteWorker(Worker worker)
        {
            workerRep.Delete(worker);
        }

        public Worker GetWorker(int id)
        {
            var worker = workerRep.Read(id);
            worker.Company = companyRep.Read(worker.CompanyID);
            return worker;
        }

        public IEnumerable<Worker> GetWorkers()
        {
            var workers = workerRep.ReadAll();
            foreach (var worker in workers) {
                worker.Company = companyRep.Read(worker.CompanyID);
            }
            return workers;
        }

        public Worker UpdateWorker(Worker worker)
        {
            worker = workerRep.Update(worker);
            worker.Company = companyRep.Read(worker.CompanyID);
            return worker;
        }
    }
}
