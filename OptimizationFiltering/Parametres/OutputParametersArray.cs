using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationFiltering.Parametres
{
    internal class OutputParametersArray
    {
        /// <summary>
        /// Температура первой перегородки
        /// </summary>
        public double Temperature1 { get; set; }

        /// <summary>
        /// Температура второй перегородки
        /// </summary>
        public double Temperature2 { get; set; }

        /// <summary>
        /// Объемный расход фильтрата
        /// </summary>
        public double VolumeFlowFiltr { get; set; }
    }
}
