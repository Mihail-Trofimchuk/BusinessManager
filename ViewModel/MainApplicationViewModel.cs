
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using BusinessManager.Views;
using BusinessManager.Model;
using BusinessManager.Model.Data;
using BusinessManager.Views.Pages;
using System;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Windows.Media.Animation;

namespace BusinessManager.ViewModel
{
    public class MainApplicationViewModel : ViewModelBase
    {
        public MainApplicationViewModel()
        {

            NotesPage = new NotesPage();
            HomePage = new HomePage();
            PaymentPage = new PaymentPage();
            SoundsPage = new SoundsPage();
            Conterpat = new Conterpaty();
            Settings = new SettingsPage();
            EntrancePage = new Views.Pages.OperationPoint.EntrancePage();
            
            PaymentPageMemu = new Views.Pages.OperationPoint.PaymentPage();
            HelpPage = new Views.PointMenu.HelpPage();
            RatesPage = new Views.PointMenu.RatesPage();
            UserAgreementPage = new Views.PointMenu.UserАgreementPage();
           


            CurrentPage = NotesPage;
            CurrentPageMenu = EntrancePage;
            UpdateTime();

        }

        
    

    private async void UpdateTime()
    {
        CurrentDate = DateTime.Now.ToString("G");
        await Task.Delay(1000);
        UpdateTime();
    }
    #region Навигация страниц

    //public static Page PaymentPage;


        private Page NotesPage;
        private Page HomePage;
        private Page PaymentPage;
        private Page SoundsPage;
        private Page EntrancePage;
        private Page MovingPage;
        private Page PaymentPageMemu;
        private Page HelpPage;
        private Page RatesPage;
        private Page UserAgreementPage;
        private Page Conterpat;
        private Page Settings;



        private Page _currentPage;
        private Page _currentPageMenu;


        public Page CurrentPage
        {
            get { return _currentPage; }
            set {
                _currentPage = value;
                RaisePropertyChanged("CurrentPage"); }
        }

        private Page pagess;
        public Page Pagess
        {
            get => pagess;
            set
            {
                pagess = value;
                RaisePropertyChanged("Pagess");
            }
        }
        public bool SetPagess(Page page)
        {
            Pagess = page;
            return true;
        }

        public Page CurrentPageMenu
        {
            get { return _currentPageMenu; }
            set { _currentPageMenu = value; RaisePropertyChanged("CurrentPageMenu"); }
        }

        /// Главные окна /////////////////////////////////////////////////////////////////////////////////////////////////////

        public ICommand bNotesPage_Click
        {
            get
            {
                return new RelayCommand(obj  => CurrentPage = NotesPage);
            }
        }

        public ICommand bHomePage_Click
        {
            get
            {
                return new RelayCommand(obj=> CurrentPage = HomePage);
            }
        }
        public ICommand bPaymentPage_Click
        {
            get
            {
                return new RelayCommand(obj => CurrentPage = PaymentPage);
            }
        }
        public ICommand bSoundsPage_Click
        {
            get
            {
                return new RelayCommand(obj => CurrentPage = SoundsPage);
            }
        }
      
        public ICommand bConterpaty_Click
        {
            get
            {
                return new RelayCommand(obj => {
                    CurrentPage = PaymentPage;
                    CurrentPage = Conterpat;


                }) ; 
            }
        }


        /// Окна операций  /////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICommand bEntrancePage_Click
        {
            get
            {
                return new RelayCommand(obj => CurrentPageMenu = EntrancePage);
            }
        }

        public ICommand bMovingPage_Click
        {
            get
            {
                return new RelayCommand(obj => CurrentPageMenu = MovingPage);
            }
        }
        public ICommand bPaymentPageMemu_Click
        {
            get
            {
                return new RelayCommand(obj => CurrentPageMenu = PaymentPageMemu);
            }
        }

        /// Окна PointMenu  /////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICommand bHelpPage_Click
        {
            get
            {
                return new RelayCommand(obj => CurrentPage = HelpPage);
            }
        }

        public ICommand bSettingsPage_Click
        {
            get
            {
                return new RelayCommand(obj => CurrentPage = Settings);
            }
        }
        public ICommand bRatesPage_Click
        {
            get
            {
                return new RelayCommand(obj => CurrentPage = RatesPage);
            }
        }
        public ICommand bUserAgreementPage_Click
        {
            get
            {
                return new RelayCommand(obj => CurrentPage = UserAgreementPage);
            }
        }
        #endregion

        #region  Управление окном
       

        private RelayCommand openMessageBox;
        public RelayCommand OpenMessageBox
        {
            get
            {
                return openMessageBox ?? new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;
                    wnd.MessageBorder.Visibility = Visibility.Visible;
                    wnd.MessageShadow.Visibility = Visibility.Visible;
                }
                );
            }
        }

        private static string message { get; set; }
        public string? Message
        {
            get { return message; }
            set
            {
                message = value;
                RaisePropertyChanged("Message");
            }
        }
        private static string title { get; set; }
        public string? Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged("Title");
            }
        }
        private RelayCommand sendMessage;
        public RelayCommand SendMessage
        {
            get
            {
                return sendMessage ?? new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;

                    if (Message == null || Message.Length < 5 || Title == null || Title.Length < 5)
                    {
                        DoubleAnimation animtion = new DoubleAnimation()
                        {
                            From = 0,
                            To = 400,
                            Duration = TimeSpan.FromSeconds(0.3),
                            FillBehavior = FillBehavior.HoldEnd,
                            AccelerationRatio = .9,
                            BeginTime = TimeSpan.FromSeconds(0.0)
                        };
                        wnd.ModelBorderError.BeginAnimation(ContentControl.WidthProperty, animtion);
                        wnd.MainModalError.Visibility = Visibility.Visible;
                        return;
                    }
                    else
                    {
                        try
                        {
                            MailAddress fromadress = new MailAddress("tvinmodels@gmail.com", "User");
                            MailAddress toadress = new MailAddress("mihatr2002@gmail.com", "Admin");
                            MailMessage message = new MailMessage(fromadress, toadress);
                            message.Body = Message;
                            message.Subject = Title;

                            SmtpClient smtpClient = new SmtpClient();
                            smtpClient.Host = "smtp.gmail.com";
                            smtpClient.Port = 587;
                            smtpClient.EnableSsl = true;
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = new NetworkCredential(fromadress.Address, "Trophimchuk2002");

                            Message = null;
                            Title = null;
                            smtpClient.Send(message);
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
                            wnd.MainModal.Visibility = Visibility.Visible;
                        }
                        catch (Exception ex)
                        {
                          
                        }



                    }
                }
                );
            }
        }

       

        private RelayCommand сloseModal;
        public RelayCommand CloseModal
        {
            get
            {
                return сloseModal ?? new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;
                    wnd.MainModal.Visibility = Visibility.Hidden;                 
                    wnd.MainModalError.Visibility = Visibility.Hidden;
                }
                );
            }
        }

        private RelayCommand closeMessages;
        public RelayCommand CloseMessages
        {
            get
            {
                return closeMessages ?? new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;
                    wnd.MessageBorder.Visibility = Visibility.Hidden;
                    wnd.MessageShadow.Visibility = Visibility.Hidden;
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
                    MainWindow wnd = obj as MainWindow;
                    wnd.Close();
                }
                );
            }
        }

        private RelayCommand Close_ClickandOpen;
        public RelayCommand btnClose_ClickAndOpen
        {
            get
            {
                return Close_ClickandOpen ?? new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;
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
                    MainWindow wnd = obj as MainWindow;
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
                    MainWindow wnd = obj as MainWindow;
                    wnd.WindowState = WindowState.Minimized;
                }
                );
            }
        }


        #endregion

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //private RelayCommand toggleSecondViewExecute;

        //public ICommand ToggleSecondViewExecute
        //{
        //    get
        //    {
        //        if (toggleSecondViewExecute == null)
        //        {
        //            toggleSecondViewExecute = new RelayCommand(obj =>PerformToggleSecondViewExecute());
        //        }

        //        return toggleSecondViewExecute;
        //    }
        //}

        //private void PerformToggleSecondViewExecute()
        //{
        //    MainWindow rg = new MainWindow();
        //    rg.Show();
        //}

        #region Свойства главной страницы

        // Имя пользователя
        
        public string UserName { get; set; } = DataWorker.GetFindUserNameById();



        // Имэйл пользователя

        public string User_Email { get; set; } = DataWorker.GetFindUserEmailById();




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
        #endregion
    }
}
