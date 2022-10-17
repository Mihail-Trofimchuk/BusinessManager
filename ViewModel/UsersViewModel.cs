
using BusinessManager.Model;
using BusinessManager.Model.Data;
using BusinessManager.Views.AdminPages;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BusinessManager.ViewModel
{
    class UsersViewModel  : INotifyPropertyChanged
    {
        public UsersViewModel()
        {


        }
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

        private ObservableCollection<Model.User> _allusers = DataWorker.GetAllUsers();
        public ObservableCollection<Model.User> AllUsers
        {
            get { return _allusers; }
            set
            {
                _allusers = value;
                RaisePropertyChanged("AllUsers");

            }
        }

        private static string searchName { get; set; }
        public string UserName
        {
            get { return searchName; }
            set
            {
                searchName = value;
                AllUsers = DataWorker.GetFindUserNameById(value);
                Users.UsersList.ItemsSource = AllUsers;
                NotifyPropertyChanged("UserName");

            }
        }

        private static string searchEmail { get; set; }
        public string UserEmail
        {
            get { return searchEmail; }
            set
            {
                searchEmail = value;
                AllUsers = DataWorker.GetFindUserEmailById(value);
                Users.UsersList.ItemsSource = AllUsers;
                NotifyPropertyChanged("UserEmail");

            }
        }

        // Команада удаления пользователя
        private RelayCommand deleteuser;
        public RelayCommand DeleteUser
        {
            get
            {
                return deleteuser ?? new RelayCommand(obj => {

                    DataWorker.DeleteUser(SelectedUser);
                    AllUsers = DataWorker.GetAllUsersById();
                    Users.UsersList.ItemsSource = null;
                    Users.UsersList.Items.Clear();
                    Users.UsersList.ItemsSource = AllUsers;
                    Users.UsersList.Items.Refresh();
                    Users.CountofUsers.Text = AllUsers.Count.ToString();

                }
                );
            }
        }

        private User selectsduser;
        public User SelectedUser
        {
            get { return selectsduser; }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectsduser = value;
                NotifyPropertyChanged("SelectedUser");
            }
        }


    }
}
