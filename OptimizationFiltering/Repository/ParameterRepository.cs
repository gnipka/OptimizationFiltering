using OptimizationFiltering.InteractionDB;
using OptimizationFiltering.Models;
using System.Collections.Generic;
using System.Linq;

namespace OptimizationFiltering.Repository
{
    internal class ParameterRepository : IRepository<Parameter>
    {
        public List<Parameter> _Parameter { get; set; }

        public ParameterRepository()
        {
            using ApplicationContext _context = new();
            _Parameter = _context.Parameter.ToList();
        }
        public void Create(Parameter item)
        {
            using ApplicationContext _context = new();
            _context.Parameter.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Parameter item)
        {
            using ApplicationContext _context = new();
            _context.Parameter.Remove(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var s = _Parameter.Find(x => x.Id == id);
            using ApplicationContext _context = new();
            _context.Parameter.Remove(s);
            _context.SaveChanges();
        }

        public Parameter Read(int id)
        {
            return _Parameter.Find(x => x.Id == id);
        }

        public void Update(Parameter item)
        {
            var s = _Parameter.Find(x => x.Id == item.Id);
            s.Id = item.Id;
            s.Name = item.Name;

            using ApplicationContext _context = new();
            _context.Parameter.Update(s);
            _context.SaveChanges();
        }
    }
}
