using OptimizationFiltering.ViewModels;
using System.Windows;

namespace OptimizationFiltering
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            DataContext = new AuthViewModel();
        }
    }
}
