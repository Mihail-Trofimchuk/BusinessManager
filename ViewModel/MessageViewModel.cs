using BusinessManager.Model;
using BusinessManager.Model.Data;
using BusinessManager.Views.AdminPages;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace BusinessManager.ViewModel
{
    class MessageViewModel : INotifyPropertyChanged
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
        public MessageViewModel()
        {

        }

        private ObservableCollection<Messages> _allMessages = DataWorker.GetAllMessages();
        public ObservableCollection<Messages> AllMessages
        {
            get { return _allMessages; }
            set
            {
                _allMessages = value;
                RaisePropertyChanged("AllMessages");

            }
        }

        private static string searchName { get; set; }
        public string UserName
        {
            get { return searchName; }
            set
            {
                searchName = value;
                AllMessages = DataWorker.GetFindMessageUserNameById(value);
                MessagePage.Messages.ItemsSource = AllMessages;
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
                AllMessages = DataWorker.GetFindMessageUserEmailById(value);
                MessagePage.Messages.ItemsSource = AllMessages;
                NotifyPropertyChanged("UserEmail");

            }
        }

        private static bool firstType { get; set; }
        public bool FirstType
        {
            get { return firstType; }
            set
            {
                firstType = value;
                NotifyPropertyChanged("FirstType");

            }
        }

        private static bool secondType { get; set; }
        public bool SecondType
        {
            get { return secondType; }
            set
            {
                secondType = value;
                NotifyPropertyChanged("SecondType");

            }
        }
        private static bool allType { get; set; }
        public bool AllType
        {
            get { return allType; }
            set
            {
                allType = value;
                NotifyPropertyChanged("ALlType");

            }
        }
        private RelayCommand nTypeChek;
        public RelayCommand NTypeChek
        {
            get
            {
                return nTypeChek ?? new RelayCommand(obj =>
                {

                    if (FirstType == true)
                    {
                        AllMessages = DataWorker.GetFindMesssagesByFirstTypeId("Настройка и внедрение");
                        MessagePage.Messages.ItemsSource = AllMessages;
                       
              
                    }
                    if (SecondType == true)
                    {
                        AllMessages = DataWorker.GetFindMesssagesByFirstTypeId("Обучение и ведение учета");
                        MessagePage.Messages.ItemsSource = AllMessages;
                    }
                    if (AllType == true)
                    {
                        AllMessages = DataWorker.GetAllMessages();
                        MessagePage.Messages.ItemsSource = AllMessages;
                    }

                }
                );
            }
        }

        // Команада удаления сообщения
        private RelayCommand deletemessage;
        public RelayCommand DeleteMessage
        {
            get
            {
                return deletemessage ?? new RelayCommand(obj => {

                    DataWorker.DeleteMessage(SelectedMessage);
                    AllMessages = DataWorker.GetAllMessages();
                    MessagePage.Messages.ItemsSource = null;
                    MessagePage.Messages.Items.Clear();
                    MessagePage.Messages.ItemsSource = AllMessages;
                    MessagePage.Messages.Items.Refresh();
                    MessagePage.CountofMessages.Text = AllMessages.Count.ToString();

                }
                );
            }
        }

        private Messages selectedMessage;
        public Messages SelectedMessage
        {
            get { return selectedMessage; }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectedMessage = value;
                NotifyPropertyChanged("SelectedMessage");
            }
        }



    }
}
