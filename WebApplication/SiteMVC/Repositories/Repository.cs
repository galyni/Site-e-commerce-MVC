using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMVC.Repositories {
    public class Repository<T> : IRepository<T> where T : class {
        private TireliresContext _context;
        private DbSet<T> table;
        public Repository() : this(new TireliresContext()) {}
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

        public T Create(T item) {        // Mettre du vrai code
            EntityEntry<T> test = table.Add(item);
            //_context.Set<T>().Add(item);
            _context.SaveChanges();
            return test.Entity;
        }

        public void Delete(T item) {
            table.Remove(item);
            _context.SaveChanges();
        }

        public void Update(T item) {
            //table.Update(item);
            table.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}

