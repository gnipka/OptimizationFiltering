using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationFiltering.Models
{
    internal class TaskParameter
    {
        /// <summary>
        /// Значение параметра для определенной задачи
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Ссылка на задачу
        /// </summary>
        public int TaskId { get; set; }
        public Task Task { get; set; }

        /// <summary>
        /// Ссылка на параметр
        /// </summary>
        public int ParameterId { get; set; }
        public Parameter Parameter { get; set; }
    }
}
