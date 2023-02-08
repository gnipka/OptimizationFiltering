using OptimizationFiltering.Equations;
using OptimizationFiltering.InteractionDB;
using OptimizationFiltering.Models;
using OptimizationFiltering.Optimization_Methods;
using OptimizationFiltering.Parametres;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_MVVM_Classes;

namespace OptimizationFiltering.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private ApplicationContext _ApplicationContext;

        public MainViewModel()
        {
            CalcMemory();
            _ApplicationContext = new ApplicationContext();
            Methods = _ApplicationContext.Method.ToList();
            Tasks = _ApplicationContext.Task.ToList();
            if (Tasks is not null)
                SelectedTask = Tasks[0];
            if (Methods is not null)
                SelectedMethod = Methods[0];
        }

        async void CalcMemory()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                while (true)
                {
                    string prcName = Process.GetCurrentProcess().ProcessName;
                    var counter = new PerformanceCounter("Process", "Working Set - Private", prcName);
                    MemoryParam = (int)(counter.RawValue / (1024 * 1024));
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

        private InputParameters _InputParameter = new InputParameters { Alpha = 1, Beta = 1, Beta1 = 1, FuelLiquid = 1, Gamma = 1, Mu = 1, Mu1 = 1 };
        public InputParameters InputParameter
        {
            get { return _InputParameter; }
            set
            {
                _InputParameter = value;
                OnPropertyChanged();
            }
        }

        private Limitations _Limitation = new Limitations { RightT1T2 = 1.5 };
        public Limitations Limitation
        {
            get { return _Limitation; }
            set
            {
                _Limitation = value;
                OnPropertyChanged();
            }
        }

        private SolutionParameters _SolutionParameter = new SolutionParameters();
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

        #region DB
        private List<Models.Task> _Tasks;
        public List<Models.Task> Tasks
        {
            get { return _Tasks; }
            set
            {
                _Tasks = value;
                OnPropertyChanged();
            }
        }

        private Models.Task _SelectedTask;
        public Models.Task SelectedTask
        {
            get { return _SelectedTask; }
            set
            {
                _SelectedTask = value;
                OnPropertyChanged();

                var parameters = _ApplicationContext.TaskParameter.Where(x => x.TaskId == SelectedTask.Id);

                foreach (var item in parameters)
                {
                    var id = item.ParameterId;
                    switch (id)
                    {
                        case 9:
                            InputParameter.CountPartitions = (int)item.Value;
                            break;
                        case 12:
                            InputParameter.DifferenceMagnitude1 = item.Value;
                            break;
                        case 13:
                            InputParameter.DifferenceMagnitude2 = item.Value;
                            break;
                        case 14:
                            SolutionParameter.CalcError = item.Value;
                            break;
                        case 15:
                            Limitation.LeftT1 = item.Value;
                            break;
                        case 16:
                            Limitation.RightT1 = item.Value;
                            break;
                        case 17:
                            Limitation.LeftT2 = item.Value;
                            break;
                        case 18:
                            Limitation.RightT2 = item.Value;
                            break;
                    }
                }
            }
        }

        private List<Method> _Methods;
        public List<Method> Methods
        {
            get { return _Methods; }
            set
            {
                _Methods = value;
                OnPropertyChanged();
            }
        }

        private Method _SelectedMethod;
        public Method SelectedMethod
        {
            get { return _SelectedMethod; }
            set
            {
                _SelectedMethod = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private ComplexMethodBox? _ComplexMethodBox = null;

        /// <summary>
        /// Модель для взаимодействия с графиком
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

        public void ProcessElement(DependencyObject element, StringBuilder sb)
        {

            if (element is TextBox)
            {
                if (Validation.GetHasError(element))
                {
                    sb.Append("ошибка:\r\n");
                }
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                ProcessElement(VisualTreeHelper.GetChild(element, i), sb);
            }
        }

        //private RelayCommand _Calc;
        //public RelayCommand Calc
        //{
        //    get
        //    {
        //        return _Calc ??= new RelayCommand(x =>
        //        {
        //            StringBuilder sb = new StringBuilder();
        //            ProcessElement((DependencyObject)x, sb);

        //            string message = sb.ToString();
        //            if (message == string.Empty)
        //            {
        //                OutputParameters parameters = new OutputParameters();

        //                if (SelectedMethod.Name == "Метод Бокса")
        //                {
        //                    _ComplexMethodBox = new ComplexMethodBox(InputParameter, Limitation, SolutionParameter);
        //                    parameters = _ComplexMethodBox.Calc();
        //                    //parameters.OutputParametersArray = FiltrationEquation.CalcEqvation(InputParameter, Limitation, SolutionParameter);

        //                    parameters.OutputParametersArray = FiltrationEquation.CalcEqvation(InputParameter, Limitation, SolutionParameter);

        //                    _DataValues = parameters.OutputParametersArray.ToList();
        //                }
        //                else if (SelectedMethod.Name == "Метод сканирования")
        //                {
        //                    ScanningMethod method = new ScanningMethod();
        //                    var points = new List<Point3D>();
        //                    method.Calculate(out points, Limitation, SolutionParameter, InputParameter);

        //                    var temp = new List<double>();

        //                    foreach (var point in points)
        //                    {
        //                        temp.Add(point.Z);
        //                    }
        //                    parameters.Temperature1Result = points.Find(x => x.Z == temp.Min()).X;
        //                    parameters.Temperature2Result = points.Find(x => x.Z == temp.Min()).Y;
        //                    parameters.VolumeFlowFiltrResult = temp.Min();
        //                    //parameters.OutputParametersArray = new OutputParametersArray[points.Count];
        //                    //_DataValues = new List<OutputParametersArray>();

        //                    //for(int i = 0; i < points.Count; i++)
        //                    //{
        //                    //    parameters.OutputParametersArray[i] = new OutputParametersArray { Temperature1 = points[i].X, Temperature2 = points[i].Y, VolumeFlowFiltr = points[i].Z };
        //                    //    _DataValues.Add(new OutputParametersArray { Temperature1 = points[i].X, Temperature2 = points[i].Y, VolumeFlowFiltr = points[i].Z });
        //                    //}

        //                    parameters.OutputParametersArray = FiltrationEquation.CalcEqvation(InputParameter, Limitation, SolutionParameter);

        //                    _DataValues = parameters.OutputParametersArray.ToList();
        //                }
        //                Diff = (int)Math.Sqrt(DataValues.Count);
        //                _MyModel = new PlotModel { Title = "C = F(T1, T2)", TitleFontSize = 16 };

        //                Func<double, double, double> peaks = (x, y) => 24 * 200 * (InputParameter.Alpha * InputParameter.FuelLiquid * Math.Pow(x * x + InputParameter.Beta * y - InputParameter.Mu * InputParameter.DifferenceMagnitude1, InputParameter.CountPartitions) + InputParameter.Gamma * Math.Pow(InputParameter.Beta1 * x + y * y - InputParameter.Mu1 * InputParameter.DifferenceMagnitude2, InputParameter.CountPartitions));

        //                double x0 = Limitation.LeftT1;
        //                double x1 = Limitation.RightT1;
        //                double y0 = Limitation.LeftT2;
        //                double y1 = Limitation.RightT2;

        //                var xx = ArrayBuilder.CreateVector(x0, x1, 100);
        //                var yy = ArrayBuilder.CreateVector(y0, y1, 100);
        //                PeaksData = ArrayBuilder.Evaluate(peaks, xx, yy);

        //                var cs = new ContourSeries
        //                {
        //                    Color = OxyColors.Black,
        //                    LabelBackground = OxyColors.White,
        //                    ColumnCoordinates = yy,
        //                    RowCoordinates = xx,
        //                    Data = PeaksData,
        //                    TrackerFormatString = "T1 = {2:0.00}, T2 = {4:0.00}" + Environment.NewLine + "V = {6:0.00}"
        //                };

        //                _MyModel.Series.Add(cs);

        //                _MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Температура на первой перегородке, С" });
        //                _MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Температура на второй перегородке, С" });

        //                MyModel = _MyModel;
        //                DataValues = _DataValues;
        //                OutputParameter = parameters;
        //            }

        //            else
        //                MessageBox.Show("Проверьте корректность введенных значений", "Ошибка при валидации данных");
        //        });
        //    }
        //}

        private RelayCommand _Calc;
        public RelayCommand Calc
        {
            get
            {
                return _Calc ??= new RelayCommand(x =>
                {
                    StringBuilder sb = new StringBuilder();
                    ProcessElement((DependencyObject)x, sb);

                    string message = sb.ToString();
                    if (message == string.Empty)
                    {

                        OutputParameters parameters = new OutputParameters();

                        if (SelectedMethod.Name == "Метод Бокса")
                        {
                            _ComplexMethodBox = new ComplexMethodBox(InputParameter, Limitation, SolutionParameter);
                            parameters = _ComplexMethodBox.Calc();
                            //parameters.OutputParametersArray = FiltrationEquation.CalcEqvation(InputParameter, Limitation, SolutionParameter);

                            parameters.OutputParametersArray = FiltrationEquation.CalcEqvation(InputParameter, Limitation, SolutionParameter);
                        }
                        else if (SelectedMethod.Name == "Метод сканирования")
                        {
                            ScanningMethod method = new ScanningMethod();
                            var points = new List<Point3D>();

                            Limitations limitation = new Limitations();

                            limitation.LeftT1 = Limitation.LeftT1;
                            limitation.RightT1 = Limitation.RightT1;
                            limitation.RightT2 = Limitation.RightT2;
                            limitation.LeftT2 = Limitation.LeftT2;
                            Limitation.RightT1T2 = Limitation.RightT1T2;

                            method.Calculate(out points, limitation, SolutionParameter, InputParameter);

                            var temp = new List<double>();

                            foreach (var point in points)
                            {
                                temp.Add(point.Z);
                            }
                            parameters.Temperature1Result = points.Find(x => x.Z == temp.Min()).X;
                            parameters.Temperature2Result = points.Find(x => x.Z == temp.Min()).Y;
                            parameters.VolumeFlowFiltrResult = temp.Min();
                            parameters.OutputParametersArray = FiltrationEquation.CalcEqvation(InputParameter, Limitation, SolutionParameter);
                        }

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
                    }
                    else
                        MessageBox.Show("Проверьте корректность введенных значений", "Ошибка при валидации данных");
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

        private AuthWindow? _AuthWindow = null;
        private AuthViewModel _AuthViewModel;

        private RelayCommand _ShowAuth;

        public RelayCommand ShowAuth
        {
            get
            {
                return _ShowAuth ??= new RelayCommand(x =>
                {
                    _AuthViewModel = new AuthViewModel();
                    _AuthWindow = new AuthWindow();
                    _AuthWindow.DataContext = _AuthViewModel;
                    _AuthWindow.Show();


                    ((Window)x).Hide();
                });
            }
        }
    }
}
