using OptimizationFiltering.InteractionDB;
using OptimizationFiltering.Models;
using System.Collections.Generic;
using System.Linq;

namespace OptimizationFiltering.Repository
{
    internal class UserRepository : IRepository<User>
    {
        public List<User> _User { get; set; }

        public UserRepository()
        {
            using ApplicationContext _context = new();
            _User = _context.User.ToList();
        }

        public void Create(User item)
        {
            using ApplicationContext _context = new();
            _context.User.Add(item);
            _context.SaveChanges();
        }

        public void Delete(User item)
        {
            using ApplicationContext _context = new();
            _context.User.Remove(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var s = _User.Find(x => x.Id == id);
            using ApplicationContext _context = new();
            _context.User.Remove(s);
            _context.SaveChanges();
        }

        public User Read(int id)
        {
            return _User.Find(x => x.Id == id);
        }

        public void Update(User item)
        {
            var s = _User.Find(x => x.Id == item.Id);
            s.Username = item.Username;
            s.Password = item.Password;
            s.RoleId = item.RoleId;
            

            using ApplicationContext _context = new();
            _context.User.Update(s);
            _context.SaveChanges();
        }
    }
}
