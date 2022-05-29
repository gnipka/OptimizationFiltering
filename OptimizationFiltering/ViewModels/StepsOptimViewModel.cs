using OptimizationFiltering.Optimization_Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_Classes;

namespace OptimizationFiltering.ViewModels
{
    internal class StepsOptimViewModel : ViewModelBase
    {
        public StepsOptimViewModel(List<Complex> complices)
        {
            Complex = complices;
            //NumberComplex = new int[complices.Count];
            //PointX = new Point[complices.Count];
            //PointY = new double[complices.Count];
            //Func = new double[complices.Count];

            //for (int i = 0; i < complices.Count; i++)
            //{
            //    NumberComplex[i] = complices[i].NumberComplex;
            //    PointX[i] = complices[i].Points;
            //    PointY = new double[complices.Count];
            //    Func = new double[complices.Count];
            //}
        }

        private List<Complex> _Complex;
        public List<Complex> Complex
        {
            get { return _Complex; }
            set
            {
                _Complex = value;
                OnPropertyChanged();
            }
        }

        //private int[] _NumberComplex;
        //public int[] NumberComplex
        //{
        //    get { return _NumberComplex; }
        //    set
        //    {
        //        _NumberComplex = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private Point[,] _PointX;
        //public Point[,] PointX
        //{
        //    get { return _PointX; }
        //    set
        //    {
        //        _PointX = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private double[] _PointY;
        //public double[] PointY
        //{
        //    get { return _PointY; }
        //    set
        //    {
        //        _PointY = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private double[] _Func;
        //public double[] Func
        //{
        //    get { return _Func; }
        //    set
        //    {
        //        _Func = value;
        //        OnPropertyChanged();
        //    }
        //}
    }
}
