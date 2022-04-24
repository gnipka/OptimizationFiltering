using OptimizationFiltering.Parametres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationFiltering.Equations
{
    internal static class FiltrationEquation
    {
        public static void CalcEqvation(InputParameters inputParameters, OutputParameters outputParameters)
        {
            outputParameters.VolumeFlowFiltr = inputParameters.Alpha * inputParameters.FuelLiquid * Math.Pow(outputParameters.Temperature1 * outputParameters.Temperature1 + inputParameters.Beta * outputParameters.Temperature2 - inputParameters.Mu * inputParameters.DifferenceMagnitude1, inputParameters.CountPartitions) + inputParameters.Gamma * Math.Pow(inputParameters.Beta1 * outputParameters.Temperature1 + outputParameters.Temperature2 * outputParameters.Temperature2 - inputParameters.Mu1 * inputParameters.DifferenceMagnitude2, inputParameters.CountPartitions);
        }
    }
}
