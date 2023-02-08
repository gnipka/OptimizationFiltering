using OptimizationFiltering.InteractionDB;
using OptimizationFiltering.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OptimizationFiltering.Repository
{
    internal class TaskParameterRepository : IRepository<TaskParameter>
    {
        public List<TaskParameter> _TaskParameter { get; set; }

        public TaskParameterRepository()
        {
            using ApplicationContext _context = new();
            _TaskParameter = _context.TaskParameter.ToList();
        }

        public void Create(TaskParameter item)
        {
            using ApplicationContext _context = new();
            _context.TaskParameter.Add(item);
            _context.SaveChanges();
        }

        public void Delete(TaskParameter item)
        {
            using ApplicationContext _context = new();
            _context.TaskParameter.Remove(item);
            _context.SaveChanges();
        }

        public void Update(TaskParameter item)
        {
            var s = _TaskParameter.First(x => x.ParameterId == item.ParameterId && x.TaskId == item.TaskId);
            s.TaskId = item.TaskId;
            s.ParameterId = item.ParameterId;
            s.Value = item.Value;

            using ApplicationContext _context = new();
            _context.TaskParameter.Update(s);
            _context.SaveChanges();
        }
        
        public TaskParameter Read(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
