using BusinessManager.Model;
using BusinessManager.Model.Data;
using BusinessManager.Views.PointMenu;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace BusinessManager.ViewModel
{
    class HelpViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public HelpViewModel()
        {

        }
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

        // Отправка сообщения пользователя
        #region Добавление
        private RelayCommand firsthelp;
        public RelayCommand FirstHelp
        {
            get
            {
                return firsthelp ?? new RelayCommand(obj =>
                {
                    HelpPage page = obj as HelpPage;
                    string resultStr = "";
                    //if (LegalEntity_Name == null || LegalEntity_Name.Replace(" ", "").Length == 0)
                    //{
                    //    MessageBox.Show("НеДобавлен");
                    //    //SetRedBlockControll(wnd, "NameBlock");
                    //}
                    //else
                    //{
                    resultStr = DataWorker.CreatMessages("Настройка и внедрение","Часовая консультация",30, SaveUser.CurrentUser);

                    DoubleAnimation animtion = new DoubleAnimation()
                    {
                        From = 0,
                        To = 400,
                        Duration = TimeSpan.FromSeconds(0.3),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .9,
                        BeginTime = TimeSpan.FromSeconds(0.0)
                    };

                    page.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                    page.HelpModal.Visibility = Visibility.Visible;
                    page.ModelBorder.Visibility = Visibility.Visible;

                }
                );
            }
        }

        private RelayCommand closeModal;
        public RelayCommand CloseModal
        {
            get
            {
                return closeModal ?? new RelayCommand(obj =>
                {
                    HelpPage page = obj as HelpPage;
                    page.HelpModal.Visibility = Visibility.Hidden;
                    page.ModelBorder.Visibility = Visibility.Hidden;
                    string resultStr = "";

                }
                );
            }
        }

        private RelayCommand secondHelp;
        public RelayCommand SecondHelp
        {
            get
            {
                return secondHelp ?? new RelayCommand(obj =>
                {
                    HelpPage page = obj as HelpPage;
                    string resultStr = "";
                    //if (LegalEntity_Name == null || LegalEntity_Name.Replace(" ", "").Length == 0)
                    //{
                    //    MessageBox.Show("НеДобавлен");
                    //    //SetRedBlockControll(wnd, "NameBlock");
                    //}
                    //else
                    //{
                    resultStr = DataWorker.CreatMessages("Настройка и внедрение", "Аудит бизнеса", 60, SaveUser.CurrentUser);
                    DoubleAnimation animtion = new DoubleAnimation()
                    {
                        From = 0,
                        To = 400,
                        Duration = TimeSpan.FromSeconds(0.3),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .9,
                        BeginTime = TimeSpan.FromSeconds(0.0)
                    };

                    page.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                    page.HelpModal.Visibility = Visibility.Visible;
                    page.ModelBorder.Visibility = Visibility.Visible;
                    //}
                }
                );
            }
        }

        private RelayCommand thirdHelp;
        public RelayCommand ThirdHelp
        {
            get
            {
                return thirdHelp ?? new RelayCommand(obj =>
                {
                    HelpPage page = obj as HelpPage;
                    string resultStr = "";
           
                    resultStr = DataWorker.CreatMessages("Настройка и внедрение", "Настройка и внедрение", 100, SaveUser.CurrentUser);
                    DoubleAnimation animtion = new DoubleAnimation()
                    {
                        From = 0,
                        To = 400,
                        Duration = TimeSpan.FromSeconds(0.3),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .9,
                        BeginTime = TimeSpan.FromSeconds(0.0)
                    };

                    page.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                    page.HelpModal.Visibility = Visibility.Visible;
                    page.ModelBorder.Visibility = Visibility.Visible;

                }
                );
            }
        }

        #endregion


    }
}
