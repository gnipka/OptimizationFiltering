using OptimizationFiltering.Parametres;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OptimizationFiltering.Optimization_Methods
{
    public class Point3D
    {
        public Point3D(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }
        public double X;
        public double Y;
        public double Z;
    }

    internal class ScanningMethod
    {
        public double _k = 10;
        public double _r = 2;
        public double _n = 2;
        public int CalculationCount { get; private set; } = 0;
        public void Calculate(out List<Point3D> points3D, Limitations limitations, SolutionParameters solutionParameters, InputParameters _InputParameters)
        {
            double funcMin = double.MaxValue;
            solutionParameters.Step = Math.Pow(_k, _r) * solutionParameters.CalcError;
            points3D = new List<Point3D>();
            var p3D = new List<Point3D>();
            List<double> values;
            Point newMin;

            newMin = SearchMinOnGrid(out p3D, out values, limitations, solutionParameters, _InputParameters);
            limitations.LeftT1 = newMin.X - solutionParameters.Step;
            limitations.LeftT2 = newMin.Y - solutionParameters.Step;

            limitations.RightT1 = newMin.X + solutionParameters.Step;
            limitations.RightT2 = newMin.Y + solutionParameters.Step;

            solutionParameters.Step /= _k;
            points3D.AddRange(p3D);

            while (funcMin > values.Min())
            {
                newMin = SearchMinOnGrid(out p3D, out values, limitations, solutionParameters, _InputParameters);

                limitations.LeftT1 = newMin.X - solutionParameters.Step;
                limitations.LeftT2 = newMin.Y - solutionParameters.Step;

                limitations.RightT1 = newMin.X + solutionParameters.Step;
                limitations.RightT2 = newMin.Y + solutionParameters.Step;

                solutionParameters.Step /= _k;
                funcMin = values.Min();
                points3D.AddRange(p3D);
            }
        }

        private Point SearchMinOnGrid(out List<Point3D> points3D, out List<double> values, Limitations limitations, SolutionParameters solutionParameters, InputParameters _InputParameters)
        {
            points3D = new List<Point3D>();

            for (var t1 = limitations.LeftT1; t1 <= limitations.RightT1; t1 += solutionParameters.Step)
                for (var t2 = limitations.LeftT2; t2 <= limitations.RightT2; t2 += solutionParameters.Step)
                {
                    if (!Conditions(t1, t2, limitations))
                        continue;
                    CalculationCount++;
                    var value = Function(t1, t2, _InputParameters);
                    points3D.Add(new Point3D(Math.Round(t1, 2), Math.Round(t2, 2), Math.Round(value, 2)));

                }

            var valuesListTemp = points3D.Select(item => item.Z).ToList();
            values = valuesListTemp;
            return new Point { X = points3D.Find(x => x.Z == valuesListTemp.Min()).X, Y = points3D.Find(x => x.Z == valuesListTemp.Min()).Y };
        }

        public double Function(double t1, double t2, InputParameters _InputParameters)
        {
            return 24 * 200 * (_InputParameters.Alpha * _InputParameters.FuelLiquid * Math.Pow(t1 * t1 + _InputParameters.Beta * t2 - _InputParameters.Mu * _InputParameters.DifferenceMagnitude1, _InputParameters.CountPartitions) + _InputParameters.Gamma * Math.Pow(_InputParameters.Beta1 * t1 + t2 * t2 - _InputParameters.Mu1 * _InputParameters.DifferenceMagnitude2, _InputParameters.CountPartitions));
        }

        private bool Conditions(double t1, double t2, Limitations limitations)
        {
            return 0.5 * t1 + t2 < limitations.RightT1T2;
        }
    }
}
