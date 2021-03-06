using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationFiltering.Parametres
{
    internal class OutputParameters
    {
        /// <summary>
        /// Oбъемный расход фильтрата 
        /// </summary>
        public double VolumeFlowFiltrResult { get; set; }

        /// <summary>
        /// Температура первой перегородки
        /// </summary>
        public double Temperature1Result { get; set; }

        /// <summary>
        /// Температура второй перегородки
        /// </summary>
        public double Temperature2Result { get; set; }

        /// <summary>
        /// массив выходных пареметров
        /// </summary>
        public OutputParametersArray[]? OutputParametersArray { get; set; }
    }
}
