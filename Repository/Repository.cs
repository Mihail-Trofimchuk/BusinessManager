using BusinessManager.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : BusinessManager.Repository.Entity.Entity, new()
    {
        private readonly ApplicationContext db;
        private readonly DbSet<T> dbSet;

        public Repository(ApplicationContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }

        public void Add(T item)
        {
            if (item == null)
                return;
            db.Entry(item).State = EntityState.Added;
            db.SaveChanges();
        }

        public void Delete(T item)
        {
            if (item == null)
                return;
            db.Remove(item);
            db.SaveChanges();
        }

        public T Get(int id) => GetAllItems.SingleOrDefault(i => i.ID == id);

        public virtual IQueryable<T> GetAllItems => dbSet;

        public void Update(T item)
        {
            if (item == null)
                return;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
