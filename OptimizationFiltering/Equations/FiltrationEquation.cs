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
        public static OutputParametersArray[] CalcEqvation(InputParameters inputParameters, Limitations limitations, SolutionParameters solutionParameters)
        {

            int sizeArray = (int)((limitations.RightT1 - limitations.LeftT1) * (limitations.RightT2 - limitations.LeftT2) / (0.1 * 0.1) +1);

            OutputParameters outputParameters = new OutputParameters();
            outputParameters.OutputParametersArray = new OutputParametersArray[sizeArray];

            int s = 0;

            for(double i = limitations.LeftT1; i < limitations.RightT1; i = Math.Round(i + 0.1, 2))
            {
                for(double j = limitations.LeftT2; j < limitations.RightT2; j = Math.Round(j + 0.1, 2))
                {
                    outputParameters.OutputParametersArray[s] = new OutputParametersArray();
                    outputParameters.OutputParametersArray[s].Temperature1 = Math.Round(i, 2);
                    outputParameters.OutputParametersArray[s].Temperature2 = Math.Round(j, 2);
                    outputParameters.OutputParametersArray[s].VolumeFlowFiltr = Math.Round(24 * 200 * (inputParameters.Alpha * inputParameters.FuelLiquid * Math.Pow(i * i + inputParameters.Beta * j - inputParameters.Mu * inputParameters.DifferenceMagnitude1, inputParameters.CountPartitions) + inputParameters.Gamma * Math.Pow(inputParameters.Beta1 * i + j * j - inputParameters.Mu1 * inputParameters.DifferenceMagnitude2, inputParameters.CountPartitions)), 2);

                    s++;
                }
            }

            return outputParameters.OutputParametersArray;
        }
    }
}
