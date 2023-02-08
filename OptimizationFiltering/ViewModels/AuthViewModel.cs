using OptimizationFiltering.InteractionDB;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_MVVM_Classes;

namespace OptimizationFiltering.ViewModels
{

    internal class AuthViewModel : ViewModelBase
    {
        ApplicationContext _applicationContext;

        public AuthViewModel()
        {
            _applicationContext = new ApplicationContext();
            BrushesLogin = System.Drawing.Color.Gray.Name.ToString();
            BrushesPass = System.Drawing.Color.Gray.Name.ToString();
        }

        private string _Login;
        public string Login
        {
            get { return _Login; }
            set
            {
                _Login = value;
                OnPropertyChanged();
            }
        }

        private string _Password;
        public string  Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }

        private string _BrushesLogin;
        public string BrushesLogin
        {
            get { return _BrushesLogin; }
            set
            {
                _BrushesLogin = value;
                OnPropertyChanged();
            }
        }
        private string _BrushesPass;
        public string BrushesPass
        {
            get { return _BrushesPass; }
            set
            {
                _BrushesPass = value;
                OnPropertyChanged();
            }
        }

        private MainWindow? _MainWindow = null;
        private MainViewModel _MainViewModel;

        private AdminWindow? _AdminWindow = null;
        private AdminViewModel _AdminViewModel;

        private RelayCommand _OpenWindow;

        public RelayCommand OpenWindow
        {
            get
            {
                return _OpenWindow ??= new RelayCommand(x =>
                {
                    PasswordBox passBox = (PasswordBox)x;
                    Password = passBox.Password;
                    var user = _applicationContext.User.ToList().FirstOrDefault(x => x.Username == Login && x.Password == Password);
                    if (user is not null)
                    {
                        if (user.RoleId == 1)
                        {
                            _MainViewModel = new MainViewModel();
                            _MainWindow = new MainWindow();
                            _MainWindow.DataContext = _MainViewModel;
                            _MainWindow.Show();
                            var parent = VisualTreeHelper.GetParent(passBox);
                            while (!(parent is Window))
                            {
                                parent = VisualTreeHelper.GetParent(parent);
                            }
                            ((Window)parent).Close();
                        }

                        else if (user.RoleId == 2)
                        {
                            _AdminViewModel = new AdminViewModel();
                            _AdminWindow = new AdminWindow();
                            _AdminWindow.DataContext = _AdminViewModel;
                            _AdminWindow.Show();
                            var parent = VisualTreeHelper.GetParent(passBox);
                            while (!(parent is Window))
                            {
                                parent = VisualTreeHelper.GetParent(parent);
                            }
                            ((Window)parent).Close();
                        }
                    }

                    if (!(_applicationContext.User.ToList().FirstOrDefault(x => x.Username == Login) is null))
                    {
                        BrushesLogin = System.Drawing.Color.Gray.Name.ToString();

                        if (_applicationContext.User.ToList().FirstOrDefault(x => x.Password == Password) is null)
                        {
                            BrushesPass = System.Drawing.Color.Red.Name.ToString();
                        }
                        else
                            BrushesPass = System.Drawing.Color.Gray.Name.ToString();
                    }
                    else
                    {
                        BrushesPass = System.Drawing.Color.Red.Name.ToString();
                        BrushesLogin = System.Drawing.Color.Red.Name.ToString();
                    }
                });
            }
        }
    }
}
