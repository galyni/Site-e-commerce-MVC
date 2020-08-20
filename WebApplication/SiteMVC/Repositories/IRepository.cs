using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMVC.Repositories {
    public interface IRepository<T> where T : class {
        T Create(T item);
        void Delete(T item);
        void Update(T item);
        T GetById(int id);
        IEnumerable<T> GetList();
    }
}
