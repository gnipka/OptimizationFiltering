using Microsoft.EntityFrameworkCore;
using OptimizationFiltering.InteractionDB;
using OptimizationFiltering.Models;
using OptimizationFiltering.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPF_MVVM_Classes;

namespace OptimizationFiltering.ViewModels
{

    internal class AdminViewModel : ViewModelBase
    {
        private ApplicationContext _ApplicationContext;

        public AdminViewModel()
        {
            Advertisement();

            //var repo = new UserRepository();

            //foreach (var user in Users)
            //{
            //    user.Role = _ApplicationContext.Role.First(x => x.Id == user.RoleId);
            //    repo.Update(user);
            //}
        }

        public void Advertisement()
        {
            _ApplicationContext = new ApplicationContext();
            _ApplicationContext.User.Load();
            Users = _ApplicationContext.User.ToList();
            Roles = _ApplicationContext.Role.ToList();
            Methods = _ApplicationContext.Method.ToList();
            Tasks = _ApplicationContext.Task.ToList();
            Parameters = _ApplicationContext.Parameter.ToList();
            TaskParameters = _ApplicationContext.TaskParameter.ToList();
        }

        #region Users
        private List<User> _Users;
        public List<User> Users
        {
            get { return _Users; }
            set
            {
                _Users = value;
                OnPropertyChanged();
            }
        }

        private User _SelectedUser;
        public User SelectedUser
        {
            get { return _SelectedUser; }
            set
            {
                _SelectedUser = value;
                OnPropertyChanged();
                if (_SelectedUser is not null)
                {
                    NewLogin = _SelectedUser.Username;
                    NewPass = _SelectedUser.Password;
                    NewRole = _SelectedUser.Role;
                }

            }
        }

        private string _NewLogin;
        public string NewLogin
        {
            get { return _NewLogin; }
            set
            {
                _NewLogin = value;
                OnPropertyChanged();
            }
        }

        private string _NewPass;
        public string NewPass
        {
            get { return _NewPass; }
            set
            {
                _NewPass = value;
                OnPropertyChanged();
            }
        }

        private List<Role> _Roles;
        public List<Role> Roles
        {
            get { return _Roles; }
            set
            {
                _Roles = value;
                OnPropertyChanged();
            }
        }

        private Role _NewRole;
        public Role NewRole
        {
            get { return _NewRole; }
            set
            {
                _NewRole = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _AddUser;

        public RelayCommand AddUser
        {
            get
            {
                return _AddUser ??= new RelayCommand(x =>
                {
                    if (NewLogin != string.Empty && NewLogin != "" && NewLogin != null && NewPass != string.Empty && NewPass != "" && NewPass != null && NewRole != null)
                    {
                        var varUser = new User { Username = NewLogin, Password = NewPass, RoleId = NewRole.Id };

                        var repo = new UserRepository();

                        repo.Create(varUser);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Заполните все поля, чтобы добавить новую запись", "Ошибка при добавлении записи");
                });
            }
        }

        private RelayCommand _UpdateUser;

        public RelayCommand UpdateUser
        {
            get
            {
                return _UpdateUser ??= new RelayCommand(x =>
                {

                    if (SelectedUser is not null)
                    {
                        var varUser = new User { Id = SelectedUser.Id, Username = NewLogin, Password = NewPass, RoleId = NewRole.Id, Role = NewRole };

                        var repo = new UserRepository();

                        repo.Update(varUser);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует изменить или проверьте, что все поля заполнены", "Ошибка при изменении записи");
                });
            }
        }

        private RelayCommand _RemoveUser;

        public RelayCommand RemoveUser
        {
            get
            {
                return _RemoveUser ??= new RelayCommand(x =>
                {
                    if (SelectedUser is not null)
                    {
                        if (Users.Count(x => x.RoleId == 2) > 1 || SelectedUser.RoleId != 2)
                        {
                            var repo = new UserRepository();

                            repo.Delete(SelectedUser);
                        }

                        else
                            MessageBox.Show("Вы не можете удалить последнего зарегистрированного администратора", "Ошибка при удалении записи");

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует удалить", "Ошибка при удалении записи");
                });
            }
        }
        #endregion

        #region Method

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
                if (_SelectedMethod is not null)
                {
                    NewNameMethod = _SelectedMethod.Name;
                    NewIsImplemented = _SelectedMethod.IsImplemented;
                }

            }
        }

        private string _NewNameMethod;
        public string NewNameMethod
        {
            get { return _NewNameMethod; }
            set
            {
                _NewNameMethod = value;
                OnPropertyChanged();
            }
        }

        private bool _NewIsImplemented;
        public bool NewIsImplemented
        {
            get { return _NewIsImplemented; }
            set
            {
                _NewIsImplemented = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _AddMethod;

        public RelayCommand AddMethod
        {
            get
            {
                return _AddMethod ??= new RelayCommand(x =>
                {
                    if (NewNameMethod != string.Empty && NewNameMethod != "" && NewNameMethod != null)
                    {
                        var varMethod = new Method { Name = NewNameMethod, IsImplemented = NewIsImplemented };

                        var repo = new MethodRepository();

                        repo.Create(varMethod);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Заполните все поля, чтобы добавить новую запись", "Ошибка при добавлении записи");
                });
            }
        }

        private RelayCommand _UpdateMethod;

        public RelayCommand UpdateMethod
        {
            get
            {
                return _UpdateMethod ??= new RelayCommand(x =>
                {

                    if (SelectedMethod is not null)
                    {
                        var varMethod = new Method { Id = SelectedMethod.Id, Name = NewNameMethod, IsImplemented = NewIsImplemented };
                        var repo = new MethodRepository();

                        repo.Update(varMethod);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует изменить или проверьте, что все поля заполнены", "Ошибка при изменении записи");
                });
            }
        }

        private RelayCommand _RemoveMethod;

        public RelayCommand RemoveMethod
        {
            get
            {
                return _RemoveMethod ??= new RelayCommand(x =>
                {
                    if (SelectedMethod is not null)
                    {
                        var repo = new MethodRepository();

                        repo.Delete(SelectedMethod);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует удалить", "Ошибка при удалении записи");
                });
            }
        }

        #endregion

        #region Task
        private List<Task> _Tasks;
        public List<Task> Tasks
        {
            get { return _Tasks; }
            set
            {
                _Tasks = value;
                OnPropertyChanged();
            }
        }

        private Task _SelectedTask;
        public Task SelectedTask
        {
            get { return _SelectedTask; }
            set
            {
                _SelectedTask = value;
                OnPropertyChanged();
                if (_SelectedTask is not null)
                {
                    NewNameTask = _SelectedTask.Name;
                }

            }
        }

        private string _NewNameTask;
        public string NewNameTask
        {
            get { return _NewNameTask; }
            set
            {
                _NewNameTask = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _AddTask;

        public RelayCommand AddTask
        {
            get
            {
                return _AddTask ??= new RelayCommand(x =>
                {
                    if (NewNameTask != string.Empty && NewNameTask != "" && NewNameTask != null)
                    {
                        var varTask = new Task { Name = NewNameTask };
                        var repo = new TaskRepository();

                        repo.Create(varTask);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Заполните все поля, чтобы добавить новую запись", "Ошибка при добавлении записи");
                });
            }
        }

        private RelayCommand _UpdateTask;

        public RelayCommand UpdateTask
        {
            get
            {
                return _UpdateTask ??= new RelayCommand(x =>
                {

                    if (SelectedTask is not null)
                    {
                        var varTask = new Task { Id= SelectedTask.Id, Name = NewNameTask }; 
                        var repo = new TaskRepository();

                        repo.Update(varTask);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует изменить или проверьте, что все поля заполнены", "Ошибка при изменении записи");
                });
            }
        }

        private RelayCommand _RemoveTask;

        public RelayCommand RemoveTask
        {
            get
            {
                return _RemoveTask ??= new RelayCommand(x =>
                {
                    if (SelectedTask is not null)
                    {
                        var repo = new TaskRepository();

                        repo.Delete(SelectedTask);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует удалить", "Ошибка при удалении записи");
                });
            }
        }
        #endregion

        #region Parameter
        private List<Parameter> _Parameters;
        public List<Parameter> Parameters
        {
            get { return _Parameters; }
            set
            {
                _Parameters = value;
                OnPropertyChanged();
            }
        }

        private Parameter _SelectedParameter;
        public Parameter SelectedParameter
        {
            get { return _SelectedParameter; }
            set
            {
                _SelectedParameter = value;
                OnPropertyChanged();
                if (_SelectedParameter is not null)
                {
                    NewNameParameter = _SelectedParameter.Name;
                }

            }
        }

        private string _NewNameParameter;
        public string NewNameParameter
        {
            get { return _NewNameParameter; }
            set
            {
                _NewNameParameter = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _AddParameter;

        public RelayCommand AddParameter
        {
            get
            {
                return _AddParameter ??= new RelayCommand(x =>
                {
                    if (NewNameParameter != string.Empty && NewNameParameter != "" && NewNameParameter != null)
                    {
                        var varParameter = new Parameter { Name = NewNameParameter };
                        var repo = new ParameterRepository();

                        repo.Create(varParameter);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Заполните все поля, чтобы добавить новую запись", "Ошибка при добавлении записи");
                });
            }
        }

        private RelayCommand _UpdateParameter;

        public RelayCommand UpdateParameter
        {
            get
            {
                return _UpdateParameter ??= new RelayCommand(x =>
                {

                    if (SelectedParameter is not null)
                    {
                        var varParameter = new Parameter { Id = SelectedParameter.Id, Name = NewNameParameter };
                        var repo = new ParameterRepository();

                        repo.Update(varParameter);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует изменить или проверьте, что все поля заполнены", "Ошибка при изменении записи");
                });
            }
        }

        private RelayCommand _RemoveParameter;

        public RelayCommand RemoveParameter
        {
            get
            {
                return _RemoveParameter ??= new RelayCommand(x =>
                {
                    if (SelectedParameter is not null)
                    {
                        var repo = new ParameterRepository();

                        repo.Delete(SelectedParameter);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует удалить", "Ошибка при удалении записи");
                });
            }
        }
        #endregion

        #region TaskParameter
        private List<TaskParameter> _TaskParameters;
        public List<TaskParameter> TaskParameters
        {
            get { return _TaskParameters; }
            set
            {
                _TaskParameters = value;
                OnPropertyChanged();
            }
        }

        private TaskParameter _SelectedTaskParameter;
        public TaskParameter SelectedTaskParameter
        {
            get { return _SelectedTaskParameter; }
            set
            {
                _SelectedTaskParameter = value;
                OnPropertyChanged();
                if (_SelectedTaskParameter is not null)
                {
                    NewParameter = _SelectedTaskParameter.Parameter;
                    NewTask = _SelectedTaskParameter.Task;
                    NewValue = _SelectedTaskParameter.Value;
                }

            }
        }

        private Parameter _NewParameter;
        public Parameter NewParameter
        {
            get { return _NewParameter; }
            set
            {
                _NewParameter = value;
                OnPropertyChanged();
            }
        }

        private Task _NewTask;
        public Task NewTask
        {
            get { return _NewTask; }
            set
            {
                _NewTask = value;
                OnPropertyChanged();
            }
        }

        private double _NewValue;
        public double NewValue
        {
            get { return _NewValue; }
            set
            {
                _NewValue = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _AddTaskParameter;

        public RelayCommand AddTaskParameter
        {
            get
            {
                return _AddTaskParameter ??= new RelayCommand(x =>
                {
                    if (NewParameter != null && NewTask != null)
                    {
                        var varTaskParameter = new TaskParameter { ParameterId = NewParameter.Id, TaskId = NewTask.Id, Value = NewValue };
                        var repo = new TaskParameterRepository();

                        repo.Create(varTaskParameter);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Заполните все поля, чтобы добавить новую запись", "Ошибка при добавлении записи");
                });
            }
        }

        private RelayCommand _UpdateTaskParameter;

        public RelayCommand UpdateTaskParameter
        {
            get
            {
                return _UpdateTaskParameter ??= new RelayCommand(x =>
                {

                    if (SelectedTaskParameter is not null)
                    {
                        var varTaskParameter = new TaskParameter { ParameterId = NewParameter.Id, TaskId = NewTask.Id, Value = NewValue };
                        var repo = new TaskParameterRepository();

                        repo.Update(varTaskParameter);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует изменить или проверьте, что все поля заполнены", "Ошибка при изменении записи");
                });
            }
        }

        private RelayCommand _RemoveTaskParameter;

        public RelayCommand RemoveTaskParameter
        {
            get
            {
                return _RemoveTaskParameter ??= new RelayCommand(x =>
                {
                    if (SelectedTaskParameter is not null)
                    {
                        var repo = new TaskParameterRepository();

                        repo.Delete(SelectedTaskParameter);

                        Advertisement();
                    }
                    else
                        MessageBox.Show("Выберите строку, которую следует удалить", "Ошибка при удалении записи");
                });
            }
        }
        #endregion


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
