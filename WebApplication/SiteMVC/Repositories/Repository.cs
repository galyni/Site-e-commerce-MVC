using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMVC.Repositories {
    public class Repository<T> : IRepository<T> where T : class {
        private TireliresContext _context;
        private DbSet<T> table;
        public Repository(TireliresContext context) {
            _context = context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetList() {
            return table;
        }

        public T GetById(int id) {
            return table.Find(id);
        }

        public void Create(T item) {        // Mettre du vrai code
            table.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id) {
            T item = table.Find(id);
            table.Remove(item);
            _context.SaveChanges();
        }

        public void Update(T item) {
            table.Update(item);
            //table.Attach(item);
            //_context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}

