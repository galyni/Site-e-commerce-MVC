using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMVC.Repositories {
    public interface IRepository<T> where T : class {
        void Create(T item);
        void Delete(int id);
        T GetById(int id);
        IEnumerable<T> GetList();
    }
}
