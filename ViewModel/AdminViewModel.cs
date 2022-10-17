
using BusinessManager.Model;
using BusinessManager.Views;
using BusinessManager.Views.AdminPages;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace BusinessManager.ViewModel
{
    class AdminViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AdminViewModel()
        {

            MassegePage = new  MessagePage();
           
           
            UsersPage  = new Users();

            //Current = UsersPage;

            UpdateTime();
        }

        private async void UpdateTime()
        {
            CurrentDate = DateTime.Now.ToString("G");
            await Task.Delay(1000);
            UpdateTime();
        }

        private string currentDate;
        public string CurrentDate
        {
            get { return currentDate; }
            set
            {
                currentDate = value;
                RaisePropertyChanged("CurrentDate");

            }
        }

        private Page UsersPage;
        private Page MassegePage;

        private Page _currentPage;

        public ICommand bUsersPage_Click
        {
            get
            {
                return new RelayCommand(obj => Admin.Users.NavigationService.Navigate(UsersPage));
            }
        }
        public ICommand bMessagePage_Click
        {
            get
            {
                return new RelayCommand(obj => Admin.Users.NavigationService.Navigate(MassegePage));
            }
        }

        public Page Current
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                RaisePropertyChanged("Current");
            }
        }
        #region Работа с окном
        private RelayCommand Close_ClickandOpen;
        public RelayCommand btnClose_ClickAndOpen
        {
            get
            {
                return Close_ClickandOpen ?? new RelayCommand(obj =>
                {
                    Admin wnd = obj as Admin;
                    wnd.Close();
                    LoginForm form = new LoginForm();
                    form.Show();
                }
                );
            }
        }

        // Изменение размеров главного окна
        private RelayCommand Restore_Click;
        public RelayCommand btnRestore_Click
        {
            get
            {
                return Restore_Click ?? new RelayCommand(obj =>
                {
                    Admin wnd = obj as Admin;
                    if (wnd.WindowState == WindowState.Normal)
                        wnd.WindowState = WindowState.Maximized;
                    else
                    {
                        wnd.WindowState = WindowState.Normal;
                    }
                }
                );
            }
        }

        // Уменьшение главного окна
        private RelayCommand Minimize_Click;
        public RelayCommand btnMinimize_Click
        {
            get
            {
                return Minimize_Click ?? new RelayCommand(obj =>
                {
                    Admin wnd = obj as Admin;
                    wnd.WindowState = WindowState.Minimized;
                }
                );
            }
        }

        // Зкрытие главного окна
        private RelayCommand Close_Click;
        public RelayCommand btnClose_Click
        {
            get
            {
                return Restore_Click ?? new RelayCommand(obj =>
                {
                    Admin wnd = obj as Admin;
                    wnd.Close();
                }
                );
            }
        }
        #endregion

    }
}
