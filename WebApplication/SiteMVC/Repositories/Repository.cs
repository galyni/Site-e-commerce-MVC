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
            Produit produit = _context.Produit.SingleOrDefault(p => p.Id == id);
            _context.Produit.Remove(produit);
            _context.SaveChanges();
        }
    }
}

