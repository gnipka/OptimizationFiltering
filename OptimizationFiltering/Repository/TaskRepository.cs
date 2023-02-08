using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OptimizationFiltering.InteractionDB;
using OptimizationFiltering.Models;

namespace OptimizationFiltering.Repository
{
    internal class TaskRepository : IRepository<Task>
    {
        public List<Task> _Task { get; set; }

        public TaskRepository()
        {
            using ApplicationContext _context = new();
            _Task = _context.Task.ToList();
        }
        public void Create(Task item)
        {
            using ApplicationContext _context = new();
            _context.Task.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Task item)
        {
            using ApplicationContext _context = new();
            _context.Task.Remove(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var s = _Task.Find(x => x.Id == id);
            using ApplicationContext _context = new();
            _context.Task.Remove(s);
            _context.SaveChanges();
        }

        public Task Read(int id)
        {
            return _Task.Find(x => x.Id == id);
        }

        public void Update(Task item)
        {
            var s = _Task.Find(x => x.Id == item.Id);
            s.Id = item.Id;
            s.Name = item.Name;           

            using ApplicationContext _context = new();
            _context.Task.Update(s);
            _context.SaveChanges();
        }
    }
}
