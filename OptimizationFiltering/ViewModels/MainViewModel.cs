using OptimizationFiltering.Equations;
using OptimizationFiltering.Optimization_Methods;
using OptimizationFiltering.Parametres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_Classes;

namespace OptimizationFiltering.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        #region [InputParameters]

        private InputParameters _InputParameter = new InputParameters { Alpha = 1, Beta = 1, Beta1 = 1, DifferenceMagnitude1 = 11, DifferenceMagnitude2 = 7, CountPartitions = 2, FuelLiquid = 1, Gamma = 1, Mu = 1, Mu1 = 1};
        public InputParameters InputParameter
        {
            get { return _InputParameter; }
            set
            {
                _InputParameter = value;
                OnPropertyChanged();
            }
        }

        private Limitations _Limitation = new Limitations { LeftT1 = -5, RightT1 = 0, LeftT2 = -1, RightT2 = 5, RightT1T2 = 1.5};
        public Limitations Limitation
        {
            get { return _Limitation; }
            set
            {
                _Limitation = value;
                OnPropertyChanged();
            }
        }

        private SolutionParameters _SolutionParameter = new SolutionParameters { CalcError = 0.01};
        public SolutionParameters SolutionParameter
        {
            get { return _SolutionParameter; }
            set
            {
                _SolutionParameter = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region [OutputParameters]

        private OutputParameters? _OutputParameter;
        public OutputParameters OutputParameter
        {
            get { return _OutputParameter; }
            set
            {
                _OutputParameter = value;
                OnPropertyChanged();
            }
        }

        

        #endregion

        private RelayCommand _Calc;
        public RelayCommand Calc
        {
            get
            {
                return _Calc ??= new RelayCommand(x =>
                {

                    OutputParameters parameters = new OutputParameters();
                    ComplexMethodBox complexMethodBox = new ComplexMethodBox(InputParameter, Limitation, SolutionParameter);
                    parameters = complexMethodBox.Calc();
                    parameters.OutputParametersArray = FiltrationEquation.CalcEqvation(InputParameter, Limitation, SolutionParameter);
                    OutputParameter = parameters;
                });
            }
        }
    }
}
