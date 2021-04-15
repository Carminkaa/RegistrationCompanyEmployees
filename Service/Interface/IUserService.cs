using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IUserService
    {
        public Worker GetWorker(int id);
        public IEnumerable<Worker> GetWorkers();
        public Worker UpdateWorker(Worker worker);
        public void DeleteWorker(Worker worker);
        public Worker CreateWorker(Worker worker);
    }
}
