using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IRepository<T>
    {
        public T Create(T item);
        public T Update(T item);
        public T Read(int id);
        public IEnumerable<T> ReadAll();
        public void Delete(T item);
    }
}
