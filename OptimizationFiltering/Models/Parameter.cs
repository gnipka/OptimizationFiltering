using System.Collections.Generic;

namespace OptimizationFiltering.Models
{
    internal class Parameter
    {
        /// <summary>
        /// Идентификатор параметра
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование параметра
        /// </summary>
        public string Name { get; set; }

        public List<TaskParameter> Parameters { get; set; }
    }
}
