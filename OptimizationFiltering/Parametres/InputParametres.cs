using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationFiltering.Parametres
{
    internal class InputParameters
    {
        /// <summary>
        /// Величина перепада давлений на первой перегородке
        /// </summary>
        public double DifferenceMagnitude1 { get; set; }

        /// <summary>
        /// Величина перепада давлений на второй перегородке
        /// </summary>
        public double DifferenceMagnitude2 { get; set; }

        /// <summary>
        /// Количество перегородок
        /// </summary>
        public int CountPartitions { get; set; }

        /// <summary>
        /// Общий  расход  фильтрующей жидкости
        /// </summary>
        public double FuelLiquid { get; set; }

        /// <summary>
        /// Нормирующий множитель альфа
        /// </summary>
        public double Alpha { get; set; }

        /// <summary>
        /// Нормирующий множитель бета
        /// </summary>
        public double Beta { get; set; }

        /// <summary>
        /// Нормирующий множитель мю
        /// </summary>
        public double Mu { get; set; }

        /// <summary>
        /// Нормирующий множитель гамма
        /// </summary>
        public double Gamma { get; set; }

        /// <summary>
        /// Нормирующий множитель бета1
        /// </summary>
        public double Beta1 { get; set; }

        /// <summary>
        /// Нормирующий множитель мю1
        /// </summary>
        public double Mu1 { get; set; }
    }
}
