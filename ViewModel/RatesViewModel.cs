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
    public class RatesViewModel : INotifyPropertyChanged
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

        // Отправка сообщения пользователя
        #region Добавление
        private RelayCommand firsthelp;
        public RelayCommand FirstHelp
        {
            get
            {
                return firsthelp ?? new RelayCommand(obj =>
                {
                    RatesPage wnd = obj as RatesPage;
                    string resultStr = "";
                    
                    resultStr = DataWorker.CreatMessages("Обучение и ведение учета", "Консультант", 60, SaveUser.CurrentUser);
                    DoubleAnimation animtion = new DoubleAnimation()
                    {
                        From = 0,
                        To = 400,
                        Duration = TimeSpan.FromSeconds(0.3),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .9,
                        BeginTime = TimeSpan.FromSeconds(0.0)
                    };

                    wnd.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                    wnd.RateModal.Visibility = Visibility.Visible;
                    wnd.ModelBorder.Visibility = Visibility.Visible;

               
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
                    RatesPage wnd = obj as RatesPage;
                    string resultStr = "";
                 
                    resultStr = DataWorker.CreatMessages("Обучение и ведение учета", "Помощник", 120, SaveUser.CurrentUser);
                    DoubleAnimation animtion = new DoubleAnimation()
                    {
                        From = 0,
                        To = 400,
                        Duration = TimeSpan.FromSeconds(0.3),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .9,
                        BeginTime = TimeSpan.FromSeconds(0.0)
                    };

                    wnd.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                    wnd.RateModal.Visibility = Visibility.Visible;
                    wnd.ModelBorder.Visibility = Visibility.Visible;
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
                    RatesPage wnd = obj as RatesPage;
                    string resultStr = "";
                  
                    resultStr = DataWorker.CreatMessages("Обучение и ведение учета", "Финансист", 160, SaveUser.CurrentUser);
                    DoubleAnimation animtion = new DoubleAnimation()
                    {
                        From = 0,
                        To = 400,
                        Duration = TimeSpan.FromSeconds(0.3),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .9,
                        BeginTime = TimeSpan.FromSeconds(0.0)
                    };

                    wnd.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                    wnd.RateModal.Visibility = Visibility.Visible;
                    wnd.ModelBorder.Visibility = Visibility.Visible;
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
                    RatesPage wnd = obj as RatesPage;
                    wnd.RateModal.Visibility = Visibility.Hidden;
                    wnd.ModelBorder.Visibility = Visibility.Hidden;
                    string resultStr = "";

                }
                );
            }
        }
        #endregion


    }
}
