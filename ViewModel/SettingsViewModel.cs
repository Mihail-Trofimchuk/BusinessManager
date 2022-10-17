using BusinessManager.Model;
using BusinessManager.Model.Data;
using BusinessManager.Views;
using BusinessManager.Views.Pages;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace BusinessManager.ViewModel
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        public SettingsViewModel()
        {
            UserEmail = DataWorker.GetFindUserEmailById();
            UserName  = DataWorker.GetFindUserNameById();
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


        public void BlueColor(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"Themes/LightTheme.xaml", UriKind.Relative);
            ResourceDictionary recource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(recource);
        }

        public void TealColor(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"Themes/TealTheme.xaml", UriKind.Relative);
            ResourceDictionary recource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(recource);
        }

        public void GreenColor(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"Themes/GreenTheme.xaml", UriKind.Relative);
            ResourceDictionary recource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(recource);
        }

        public void RedColor(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"Themes/RedTheme.xaml", UriKind.Relative);
            ResourceDictionary recource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(recource);
        }
        public void OrangeColor(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"Themes/OrangeTheme.xaml", UriKind.Relative);
            ResourceDictionary recource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(recource);
        }
        public void YellowColor(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"Themes/YellowTheme.xaml", UriKind.Relative);
            ResourceDictionary recource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(recource);
        }
        public void PurpleColor(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"Themes/PurpleTheme.xaml", UriKind.Relative);
            ResourceDictionary recource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(recource);
        }
        public void BrownColor(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"Themes/BrownTheme.xaml", UriKind.Relative);
            ResourceDictionary recource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(recource);
        }
        public void SilverColor(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"Themes/SilverTheme.xaml", UriKind.Relative);
            ResourceDictionary recource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(recource);
        }

        public static string entranceArticle { get; set; }
        public string EntranceArticle
        {
            get { return entranceArticle; }
            set
            {
                entranceArticle = value;
                RaisePropertyChanged("EntranceArticle");
            }
        }

        public static Article selectEntrnceArticle { get; set; }
        public Article SelectEntrnceArticle
        {
            get { return selectEntrnceArticle; }
            set
            {
                selectEntrnceArticle = value;
                RaisePropertyChanged("SelectEntrnceArticle");
            }
        }

        public static Article selectPaymentArticle { get; set; }
        public Article SelectPaymentArticle
        {
            get { return selectPaymentArticle; }
            set
            {
                selectPaymentArticle = value;
                RaisePropertyChanged("SelectPaymentArticle");
            }
        }

        public static string paymentArticle { get; set; }
        public string PaymentArticle
        {
            get { return paymentArticle; }
            set
            {
                paymentArticle = value;
                RaisePropertyChanged("PaymentArticle");
            }
        }

        public static string username { get; set; } = DataWorker.GetFindUserNameById();
        public string UserName
        {
            get { return username; }
            set
            {
                username = value;
                RaisePropertyChanged("UserName");
            }
        }

        public static string useremail { get; set; } = DataWorker.GetFindUserEmailById();
        public string UserEmail
        {
            get { return useremail; }
            set
            {
                useremail = value;
                RaisePropertyChanged("UserEmail");
            }
        }

        public static string userpassword { get; set; }
        public string UserPassword
        {
            get { return userpassword; }
            set
            {
                userpassword = value;
                RaisePropertyChanged("UserPassword");
            }
        }

        private RelayCommand changeUser;
        public RelayCommand ChangeUser
        {
            get
            {
                return changeUser ?? new RelayCommand(obj =>
                {
                    SettingsPage wnd = obj as SettingsPage;
                    if (UserPassword != null)
                    {
                        if (Regex.IsMatch(UserPassword, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{1,16}$"))
                        {
                            DataWorker.EditUser(SaveUser.CurrentUser, UserName, UserPassword, UserEmail);

                            MainWindow.UserN.Text = DataWorker.GetFindUserNameById();
                            MainWindow.UserE.Content = DataWorker.GetFindUserEmailById();

                            UserName = null;
                            UserPassword = null;
                            UserEmail = null;

                            DoubleAnimation animtion = new DoubleAnimation()
                            {
                                From = 0,
                                To = 350,
                                Duration = TimeSpan.FromSeconds(0.3),
                                FillBehavior = FillBehavior.HoldEnd,
                                AccelerationRatio = .9,
                                BeginTime = TimeSpan.FromSeconds(0.0)
                            };
                            wnd.ModelBorderUser.Visibility = Visibility.Visible;
                            wnd.ModelBorderUser.BeginAnimation(ContentControl.WidthProperty, animtion);
                            wnd.LoginModalUser.Visibility = Visibility.Visible;
                            return;

                            MessageBox.Show("Изменен");
                        }
                        else
                        {
                            DoubleAnimation animtion = new DoubleAnimation()
                            {
                                From = 0,
                                To = 350,
                                Duration = TimeSpan.FromSeconds(0.3),
                                FillBehavior = FillBehavior.HoldEnd,
                                AccelerationRatio = .9,
                                BeginTime = TimeSpan.FromSeconds(0.0)
                            };
                            wnd.ModelBorder.Visibility = Visibility.Visible;
                            wnd.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                            wnd.LoginModal.Visibility = Visibility.Visible;
                            return;
                        }

                    }
                    else 
                    {
                        DoubleAnimation animtion = new DoubleAnimation()
                        {
                            From = 0,
                            To = 350,
                            Duration = TimeSpan.FromSeconds(0.3),
                            FillBehavior = FillBehavior.HoldEnd,
                            AccelerationRatio = .9,
                            BeginTime = TimeSpan.FromSeconds(0.0)
                        };

                        wnd.ModelBorder.Visibility = Visibility.Visible;
                        wnd.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                        wnd.LoginModal.Visibility = Visibility.Visible;
                        return;
                    }
                }
                );
            }
        }



        public ObservableCollection<Article> EntranceArticles { get; set; } = DataWorker.GetAllEntranceArticle();
        public ObservableCollection<Article> PaymentArticles { get; set; } = DataWorker.GetAllPaymentArticle();


        private RelayCommand deletePaymentArticle;
        public RelayCommand DeletePaymentArticle
        {
            get
            {
                return deletePaymentArticle  ?? new RelayCommand(obj =>
                {

                    DataWorker.DeletePaymentArticle(SelectPaymentArticle);
                    

                    SettingsPage.ListofP.ItemsSource = null;
                    SettingsPage.ListofP.Items.Clear();
                    SettingsPage.ListofP.ItemsSource = DataWorker.GetAllPaymentArticle();
                    SettingsPage.ListofP.Items.Refresh();

                    HomePage.ArticleListP.ItemsSource = DataWorker.GetAllPaymentArticle();
                    HomePage.ArticleListP.Items.Refresh();
                   
                }
                );
            }
        }

        private RelayCommand closeModal1;
        public RelayCommand CloseModal1
        {
            get
            {
                return closeModal1 ?? new RelayCommand(obj =>
                {
                    SettingsPage wnd = obj as SettingsPage;
                    wnd.ModelBorder.Visibility = Visibility.Hidden;
                    wnd.LoginModal.Visibility = Visibility.Hidden;

                }
                );
            }
        }

        private RelayCommand closeModal2;
        public RelayCommand CloseModal2
        {
            get
            {
                return closeModal2 ?? new RelayCommand(obj =>
                {
                    SettingsPage wnd = obj as SettingsPage;
                    wnd.ModelBorderUser.Visibility = Visibility.Hidden; 
                    wnd.LoginModalUser.Visibility = Visibility.Hidden;

                }
                );
            }
        }

        private RelayCommand deleteEntranceArticle;
        public RelayCommand DeleteEntranceArticle
        {
            get
            {
                return deleteEntranceArticle ?? new RelayCommand(obj =>
                {


                    DataWorker.DeleteEntranceArticle(SelectEntrnceArticle);

                    SettingsPage.ListofE.ItemsSource = null;
                    SettingsPage.ListofE.Items.Clear();
                    SettingsPage.ListofE.ItemsSource = DataWorker.GetAllEntranceArticle();
                    SettingsPage.ListofE.Items.Refresh();

                    HomePage.ArticleList.ItemsSource = null;
                    HomePage.ArticleList.Items.Clear();
                    HomePage.ArticleList.ItemsSource = DataWorker.GetAllEntranceArticle();
                    HomePage.ArticleList.Items.Refresh();

                }
                );
            }
        }

        private RelayCommand addEntranceArticle;
        public RelayCommand AddEntranceArticle
        {
            get
            {
                return addEntranceArticle ?? new RelayCommand(obj =>
                {
                    SettingsPage page = obj as SettingsPage;

                    DataWorker.CreateEntranceArticle(EntranceArticle);
                    page.AllEntranceArticles.ItemsSource = null;
                    page.AllEntranceArticles.Items.Clear();
                    page.AllEntranceArticles.ItemsSource = DataWorker.GetAllEntranceArticle();
                    page.AllEntranceArticles.Items.Refresh();
                  
                    HomePage.ArticleList.ItemsSource = DataWorker.GetAllEntranceArticle();
                    HomePage.ArticleList.Items.Refresh();
                    EntranceArticle = null;
                }
                );
            }
        }

        private RelayCommand addPaymentArticle;
        public RelayCommand AddPaymentArticle
        {
            get
            {
                return addPaymentArticle ?? new RelayCommand(obj =>
                {
                    SettingsPage page = obj as SettingsPage;
                    DataWorker.CreatePaymentArticle(PaymentArticle);
                    page.AllPaymentArticle.ItemsSource = null;
                    page.AllPaymentArticle.Items.Clear();
                    page.AllPaymentArticle.ItemsSource = DataWorker.GetAllPaymentArticle();
                    page.AllPaymentArticle.Items.Refresh();
                  
                    HomePage.ArticleListP.ItemsSource = DataWorker.GetAllPaymentArticle();
                    HomePage.ArticleListP.Items.Refresh();
                    PaymentArticle = null;
                }
                );
            }
        }



    }
}
