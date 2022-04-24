using MathNet.Symbolics;
using OptimizationFiltering.Parametres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationFiltering.Optimization_Methods
{
    internal static class SteepestDescent
    {
        private static Expression CalculationPartialDerivative(InputParameters inputParameters, string param)
        {
            Expression expr = Infix.ParseOrThrow($"(x^2 + y - {inputParameters.DifferenceMagnitude1})^{inputParameters.CountPartitions} + (x + y^2 - {inputParameters.DifferenceMagnitude2})^{inputParameters.CountPartitions}");
            return Calculus.Differentiate(Expression.Symbol($"{param}"), expr);
        }

        public static void Calc(InputParameters inputParameters, Limitations limitations, OutputParameters outputParameters)
        {
            var variables = new Dictionary<string, FloatingPoint>
            {
                {"x", limitations.LeftT1 },
                {"y", limitations.LeftT2 }
            };

            var derivativeT1 = CalculationPartialDerivative(inputParameters, "x"); // производная по Т1
            string str = Infix.Format(derivativeT1);
            var derivativeT2 = CalculationPartialDerivative(inputParameters, "y"); // производная по Т2
            var str2 = Infix.Format(derivativeT2);
            double resultX01 = Evaluate.Evaluate(variables, derivativeT1).RealValue; // значение производной по Т1 в начальной точке
            double resultX02 = Evaluate.Evaluate(variables, derivativeT2).RealValue; // значение производной по Т2 в начальной точке

            // выражение для следующей точки с неизвестным шагом альфа (x1(alpha) = x0 - alpha * gradF(x0))
            Expression expr1 = Infix.ParseOrThrow($"{limitations.LeftT1} - alpha * {resultX01}");
            Expression expr2 = Infix.ParseOrThrow($"{limitations.LeftT2} - alpha * {resultX02}");


            var strExpr1 = Infix.Format(derivativeT1);
            strExpr1 = strExpr1.Replace("x", Infix.Format(expr1));
            strExpr1 = strExpr1.Replace("y", Infix.Format(expr2));
            Expression newDerivativeT1 = Infix.ParseOrThrow(strExpr1);

            //Expression resultX11 = Calculus.NormalLine(Expression.Symbol("x"), expr1, derivativeT1);
            //Expression resultX12 = Calculus.NormalLine(Expression.Symbol("y"), expr2, resultX11);
            //string str3 = Infix.Format(resultX12);
            //Expression expr = Infix.ParseOrThrow(str3);

            var variables1 = new Dictionary<string, FloatingPoint>
            {
                {"alpha", 0 },
            };
            while (true)
            {
                var res = Evaluate.Evaluate(variables1, newDerivativeT1).RealValue;
                if ((int)res == 0)
                {
                    break;
                }
                var newVal = variables1["alpha"].RealValue + 0.000001;
                variables1 = new Dictionary<string, FloatingPoint>
                {
                    {"alpha", newVal },
                };
            }
            variables1["alpha"].RealValue.ToString();
        }
    }
}
