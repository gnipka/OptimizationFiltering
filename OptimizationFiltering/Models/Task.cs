using System.Collections.Generic;

namespace OptimizationFiltering.Models
{
    internal class Task
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование задачи
        /// </summary>
        public string Name { get; set; }
        public List<TaskParameter> Parameters { get; set; }
    }
}
