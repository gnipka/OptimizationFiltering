using OptimizationFiltering.InteractionDB;
using OptimizationFiltering.Models;
using System.Collections.Generic;
using System.Linq;

namespace OptimizationFiltering.Repository
{
    internal class MethodRepository : IRepository<Method>
    {
        public List<Method> _Method { get; set; }

        public MethodRepository()
        {
            using ApplicationContext _context = new();
            _Method = _context.Method.ToList();
        }

        public void Create(Method item)
        {
            using ApplicationContext _context = new();
            _context.Method.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Method item)
        {
            using ApplicationContext _context = new();
            _context.Method.Remove(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var s = _Method.Find(x => x.Id == id);
            using ApplicationContext _context = new();
            _context.Method.Remove(s);
            _context.SaveChanges();
        }

        public Method Read(int id)
        {
            return _Method.Find(x => x.Id == id);
        }

        public void Update(Method item)
        {
            var s = _Method.Find(x => x.Id == item.Id);
            s.Id = item.Id;
            s.Name = item.Name;
            s.IsImplemented = item.IsImplemented;


            using ApplicationContext _context = new();
            _context.Method.Update(s);
            _context.SaveChanges();
        }
    }
}
