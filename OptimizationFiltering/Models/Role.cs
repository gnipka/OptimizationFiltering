using System.Collections.Generic;

namespace OptimizationFiltering.Models
{
    internal class Role
    {
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
