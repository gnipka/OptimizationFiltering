using OptimizationFiltering.Equations;
using OptimizationFiltering.Optimization_Methods;
using OptimizationFiltering.Parametres;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_MVVM_Classes;

namespace OptimizationFiltering.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            CalcMemory();
        }

        async void CalcMemory()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    string prcName = Process.GetCurrentProcess().ProcessName;
                    var counter = new PerformanceCounter("Process", "Working Set - Private", prcName);
                    MemoryParam = (int)(counter.RawValue / (1024* 1024));
                    //MemoryParam = (int)System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024;
                }
            });
        }

        private int _MemoryParam;
        public int MemoryParam
        {
            get { return _MemoryParam; }
            set
            {
                _MemoryParam = value;
                OnPropertyChanged();
            }
        }

        #region [InputParameters]

        private InputParameters _InputParameter = new InputParameters { Alpha = 1, Beta = 1, Beta1 = 1, DifferenceMagnitude1 = 11, DifferenceMagnitude2 = 7, CountPartitions = 2, FuelLiquid = 1, Gamma = 1, Mu = 1, Mu1 = 1 };
        public InputParameters InputParameter
        {
            get { return _InputParameter; }
            set
            {
                _InputParameter = value;
                OnPropertyChanged();
            }
        }

        private Limitations _Limitation = new Limitations { LeftT1 = -5, RightT1 = 0, LeftT2 = -1, RightT2 = 5, RightT1T2 = 1.5 };
        public Limitations Limitation
        {
            get { return _Limitation; }
            set
            {
                _Limitation = value;
                OnPropertyChanged();
            }
        }

        private SolutionParameters _SolutionParameter = new SolutionParameters { CalcError = 0.1 };
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


        private ComplexMethodBox? _ComplexMethodBox = null;

        /// <summary>
        /// Модель для взаимоействия с графиком
        /// </summary>
        private PlotModel _MyModel;

        public PlotModel MyModel
        {
            get
            {
                return _MyModel;
            }
            set
            {
                _MyModel = value;
                OnPropertyChanged();
            }
        }

        private double[,] _PeaksData;
        public double[,] PeaksData
        {
            get { return _PeaksData; }
            set { _PeaksData = value; OnPropertyChanged(); }
        }

        private int _Diff;
        public int Diff
        {
            get { return _Diff; }
            set { _Diff = value; OnPropertyChanged(); }
        }



        private List<OutputParametersArray> _DataValues;
        public List<OutputParametersArray> DataValues
        {
            get { return _DataValues; }
            set { _DataValues = value; OnPropertyChanged(); }
        }

        private RelayCommand _Calc;
        public RelayCommand Calc
        {
            get
            {
                return _Calc ??= new RelayCommand(x =>
                {

                    OutputParameters parameters = new OutputParameters();
                    _ComplexMethodBox = new ComplexMethodBox(InputParameter, Limitation, SolutionParameter);
                    parameters = _ComplexMethodBox.Calc();
                    //parameters.OutputParametersArray = FiltrationEquation.CalcEqvation(InputParameter, Limitation, SolutionParameter);

                    parameters.OutputParametersArray = FiltrationEquation.CalcEqvation(InputParameter, Limitation, SolutionParameter);
                    

                    //foreach (var item in parameters.OutputParametersArray)
                    //{
                    //    _DataValues.Add(new OutputParametersArray { Temperature1 = (int)item.Temperature1, Temperature2 = (int)item.Temperature2, VolumeFlowFiltr = (int)item.VolumeFlowFiltr });
                    //}

                    DataValues = parameters.OutputParametersArray.ToList();
                    Diff = (int)Math.Sqrt(DataValues.Count);

                    _MyModel = new PlotModel { Title = "C = F(T1, T2)", TitleFontSize = 16 };

                    Func<double, double, double> peaks = (x, y) => 24 * 200 * (InputParameter.Alpha * InputParameter.FuelLiquid * Math.Pow(x * x + InputParameter.Beta * y - InputParameter.Mu * InputParameter.DifferenceMagnitude1, InputParameter.CountPartitions) + InputParameter.Gamma * Math.Pow(InputParameter.Beta1 * x + y * y - InputParameter.Mu1 * InputParameter.DifferenceMagnitude2, InputParameter.CountPartitions));

                    double x0 = Limitation.LeftT1;
                    double x1 = Limitation.RightT1;
                    double y0 = Limitation.LeftT2;
                    double y1 = Limitation.RightT2;

                    var xx = ArrayBuilder.CreateVector(x0, x1, 100);
                    var yy = ArrayBuilder.CreateVector(y0, y1, 100);
                    PeaksData = ArrayBuilder.Evaluate(peaks, xx, yy);

                    var cs = new ContourSeries
                    {
                        Color = OxyColors.Black,
                        LabelBackground = OxyColors.White,
                        ColumnCoordinates = yy,
                        RowCoordinates = xx,
                        Data = PeaksData,
                        TrackerFormatString = "T1 = {2:0.00}, T2 = {4:0.00}" + Environment.NewLine + "V = {6:0.00}"
                    };

                    _MyModel.Series.Add(cs);

                    _MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Температура на первой перегородке, С" });
                    _MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Температура на второй перегородке, С" });

                    MyModel = _MyModel;

                    OutputParameter = parameters;

                });
            }
        }

        private StepsOptim? _StepsOptim = null;
        private StepsOptimViewModel _StepsOptimViewModel;

        private RelayCommand _OpenStepsOptim;

        public RelayCommand OpenStepsOptim
        {
            get
            {
                return _OpenStepsOptim ??= new RelayCommand(x =>
                {

                    _StepsOptimViewModel = new StepsOptimViewModel(_ComplexMethodBox.Complices);
                    _StepsOptim = new StepsOptim();
                    _StepsOptim.DataContext = _StepsOptimViewModel;
                    _StepsOptim.Show();

                });
            }
        }

    }
}
