using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repositories
{
    public class Repository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetByName(string name)
        {
            return _context.Set<T>().FirstOrDefault(entity => EF.Property<string>(entity, "UName") == name ||
                                                              EF.Property<string>(entity, "BName") == name ||
                                                              EF.Property<string>(entity, "CName") == name);
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void UpdateByName(string name, Action<T> updateAction)
        {
            var entity = GetByName(name);
            if (entity != null)
            {
                updateAction(entity);
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
