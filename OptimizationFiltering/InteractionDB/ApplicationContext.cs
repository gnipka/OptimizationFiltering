using Microsoft.EntityFrameworkCore;
using OptimizationFiltering.Models;

namespace OptimizationFiltering.InteractionDB
{
    internal class ApplicationContext : DbContext
    {
        public string ConnectionString;

        public ApplicationContext()
        {
            ConnectionString = "Data Source=OptimizationFiltering.db";
        }

        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Method> Method { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Parameter> Parameter { get; set; }
        public DbSet<TaskParameter> TaskParameter { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var role = new Role
            {
                Id = 1,
                Name = "Пользователь",
            };

            var role2 = new Role
            {
                Id = 2,
                Name = "Администратор",
            };

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "user",
                Password = "user",
                RoleId = role.Id
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                Username = "admin",
                Password = "admin",
                RoleId = role2.Id
            });

            modelBuilder.Entity<Role>().HasData(role);

            modelBuilder.Entity<Role>().HasData(role2);

            modelBuilder.Entity<Method>().HasData(new Method
            {
                Id = 1,
                Name = "Метод Бокса",
                IsImplemented = true
            });

            modelBuilder.Entity<Task>().HasData(new Task
            {
                Id = 1,
                Name = "Вариант №20"
            });

            modelBuilder.Entity<Parameter>().HasData(new Parameter
            {
                Id = 1,
                Name = "Количество перегородок"
            });

            modelBuilder.Entity<Parameter>().HasData(new Parameter
            {
                Id = 2,
                Name = "Величина перепада давлений на первой перегородке"
            });

            modelBuilder.Entity<Parameter>().HasData(new Parameter
            {
                Id = 3,
                Name = "Величина перепада давлений на второй перегородке"
            });

            modelBuilder.Entity<Parameter>().HasData(new Parameter
            {
                Id = 4,
                Name = "Погрешность вычисления"
            });

            modelBuilder.Entity<Parameter>().HasData(new Parameter
            {
                Id = 5,
                Name = "Минимальная температура на первой перегородке"
            });

            modelBuilder.Entity<Parameter>().HasData(new Parameter
            {
                Id = 6,
                Name = "Максимальная температура на первой перегородке"
            });

            modelBuilder.Entity<Parameter>().HasData(new Parameter
            {
                Id = 7,
                Name = "Минимальная температура на второй перегородке"
            });

            modelBuilder.Entity<Parameter>().HasData(new Parameter
            {
                Id = 8,
                Name = "Максимальная температура на второй перегородке"
            });

            modelBuilder.Entity<TaskParameter>().HasKey(x => new { x.ParameterId, x.TaskId });

            modelBuilder.Entity<TaskParameter>().HasData(new TaskParameter
            {
                ParameterId = 1,
                TaskId = 1,
                Value = 2,
            });

            modelBuilder.Entity<TaskParameter>().HasData(new TaskParameter
            {
                ParameterId = 2,
                TaskId = 1,
                Value = 11,
            });

            modelBuilder.Entity<TaskParameter>().HasData(new TaskParameter
            {
                ParameterId = 3,
                TaskId = 1,
                Value = 7,
            });

            modelBuilder.Entity<TaskParameter>().HasData(new TaskParameter
            {
                ParameterId = 4,
                TaskId = 1,
                Value = -5,
            });

            modelBuilder.Entity<TaskParameter>().HasData(new TaskParameter
            {
                ParameterId = 5,
                TaskId = 1,
                Value = 0,
            });

            modelBuilder.Entity<TaskParameter>().HasData(new TaskParameter
            {
                ParameterId = 6,
                TaskId = 1,
                Value = -1,
            });

            modelBuilder.Entity<TaskParameter>().HasData(new TaskParameter
            {
                ParameterId = 7,
                TaskId = 1,
                Value = 5,
            });

            modelBuilder.Entity<TaskParameter>().HasData(new TaskParameter
            {
                ParameterId = 8,
                TaskId = 1,
                Value = 1.5,
            });
        }
    }
}