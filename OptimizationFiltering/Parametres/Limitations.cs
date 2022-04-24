using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationFiltering.Parametres
{
    internal class Limitations
    {
        /// <summary>
        /// Ограничение слева температуры 1
        /// </summary>
        public double LeftT1 { get; set; }

        /// <summary>
        /// Ограничение справа температуры 1
        /// </summary>
        public double RightT1 { get; set; }

        /// <summary>
        /// Ограничение слева температуры 2
        /// </summary>
        public double LeftT2 { get; set; }

        /// <summary>
        /// Ограничение справа температуры 2
        /// </summary>
        public double RightT2 { get; set; }

        /// <summary>
        /// Ограничение на сумму T1 и T2
        /// </summary>
        public double RightT1T2 { get; set; }
    }
}
