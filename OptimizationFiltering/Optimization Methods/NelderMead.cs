using OptimizationFiltering.Parametres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationFiltering.Optimization_Methods
{
    public static class Constants
    {
        public const double DoubleComparisonDelta = 0.0000001;
    }

    public class Derivative
    {
        private const double DEFAULT_DELTA = 0.0000001F;

        public double GetDerivative(Func<double, double> function, double point)
        {
            return GetDerivative(function, point, DEFAULT_DELTA);
        }

        public double GetDerivative(Func<double, double> function, double point, double delta)
        {
            return (function(point + delta) - function(point - delta)) / (2 * delta);
        }
    }

    internal class NelderMead
    {

        private List<double> CopyPointWithReplace(List<double> point, double replace, int replaceIndex)
        {
            var result = new List<double>();
            for (var i = 0; i < point.Count; i++)
                if (i == replaceIndex)
                    result.Add(replace);
                else
                    result.Add(point[i]);

            return result;
        }

        public List<double> Calculate(List<double> startPoint, Func<List<double>, double> function)
        {
            double alpha = 1;
            var alphaDecreaseRate = 0.9;
            var currentPoint = startPoint;
            while (true)
            {
                var currentValue = function(currentPoint);
                var newPoint = new List<double>();
                for (var i = 0; i < currentPoint.Count; i++)
                {
                    Func<double, double> func = x => function(CopyPointWithReplace(currentPoint, x, i));
                    newPoint.Add(currentPoint[i] - alpha * (1.0 / Convert.ToDouble(startPoint.Count)) * new Derivative().GetDerivative(func, currentPoint[i]));
                }
                var newValue = function(newPoint);

                if (newValue > currentValue)
                    alpha *= alphaDecreaseRate;
                else
                {
                    if (currentValue - newValue <= Constants.DoubleComparisonDelta)
                        return newPoint;
                    else
                        currentPoint = newPoint;
                }
            }

        }


    }
}
