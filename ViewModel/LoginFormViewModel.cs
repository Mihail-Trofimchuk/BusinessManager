using BusinessManager.Model;
using BusinessManager.Model.Data;
using BusinessManager.Views;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BusinessManager.ViewModel
{
    public class LoginFormViewModel : INotifyPropertyChanged
    {

        public LoginFormViewModel()
        {
            
            SetNullValuesToProperties();
           


        }

        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {         
            Regex regex = new Regex(@"^[a-zA-Z](.[a-zA-Z0-9_-]*)$");
            regex.Replace(" ", "");
            e.Handled = regex.IsMatch(e.Text);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Добавление пользователя

        public static string name { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public static string password { get; set; }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged("Password");
            }
        }

        private void SetNullValuesToProperties()
        {
            //для пользователя
            Name = "";
            Password = "";
            Email  = "";
        }

        public static string email { get; set; }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                NotifyPropertyChanged("Email");
            }
        }

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        private RelayCommand addNewUser;
        public RelayCommand AddNewUser
        {
            get
            {
                return addNewUser ?? new RelayCommand(obj =>
                {
                    LoginForm wnd = obj as LoginForm;
             
                    string resultStr = "";

                    if (Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{1,16}$"))
                    {
                        resultStr = DataWorker.CreateUser(Name, Password, Email);
                        if (resultStr == "Уже существует")
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

                            wnd.ModelBorderUser.BeginAnimation(ContentControl.WidthProperty, animtion);
                            wnd.LoginModalUser.Visibility = Visibility.Visible;
                            return;
                        }
                       
                        wnd.Login.Visibility = Visibility.Visible;
                        wnd.button.Visibility = Visibility.Visible;
                        wnd.Login_Copy.Visibility = Visibility.Hidden;
                        wnd.button2.IsHitTestVisible = false;
                        SetNullValuesToProperties();
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
                        
                        wnd.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                        wnd.LoginModal.Visibility = Visibility.Visible;
                        return;
                    }

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
                    LoginForm wnd = obj as LoginForm;
                    wnd.LoginModal.Visibility = Visibility.Hidden;
                    wnd.LoginModalUser.Visibility = Visibility.Hidden;
                    string resultStr = "";

                }
                );
            }
        }

        private RelayCommand closeRegGrid;
        public RelayCommand CloseRegGrid
        {
            get
            {
                return closeRegGrid ?? new RelayCommand(obj =>
                {
                    LoginForm wnd = obj as LoginForm;
                    wnd.Login.Visibility = Visibility.Visible;
                    wnd.button.Visibility = Visibility.Visible;
                    wnd.Login_Copy.Visibility = Visibility.Hidden;
                    wnd.Back.Visibility = Visibility.Hidden;
                    SetNullValuesToProperties();
                    string resultStr = "";

                }
                );
            }
        }

        private RelayCommand regGrid;
        public RelayCommand RegGrid
        {
            get
            {
                return regGrid ?? new RelayCommand(obj =>
                {
                     LoginForm wnd = obj as LoginForm;
                    wnd.Login.Visibility = Visibility.Hidden;
                    wnd.button.Visibility = Visibility.Hidden;
                    wnd.Login_Copy.Visibility = Visibility.Visible;
                    wnd.Back.Visibility = Visibility.Visible;
                    SetNullValuesToProperties();
                    string resultStr = "";

                }
                );
            }
        }


        #region

        public static string checkEmail { get; set; }
        public string CheckEmail
        {
            get { return checkEmail; }
            set
            {
                checkEmail = value;
                NotifyPropertyChanged("CheckEmail");
            }
        }

        public static string checkPassword { get; set; }
        public string CheckPassword
        {
            get { return checkPassword; }
            set
            {
                checkPassword = value;
                NotifyPropertyChanged("CheckPassword");
            }
        }

        public static string newPassword { get; set; }
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                newPassword = value;
                NotifyPropertyChanged("NewPassword");
            }
        }
        

        private static string Code { get; set; }

        public void ChangepasswordEmail(object sender, RoutedEventArgs e)// add the proper parameters
        {

            LoginForm.ChangePasswordGrid.Visibility = Visibility.Visible;
            LoginForm.ChangePasswordGridB.Visibility = Visibility.Visible;
        }

        private RelayCommand sendPassword;
        public RelayCommand SendPassword
        {
            get
            {
                return sendPassword ?? new RelayCommand(obj =>
                {
                    LoginForm wnd = obj as LoginForm;
                    try {
                      
                        if (DataWorker.UserExist(CheckEmail))
                        {
                            Random rnd = new Random();
                            Code = rnd.Next(1000, 10000).ToString();
                            MailAddress fromadress = new MailAddress("tvinmodels@gmail.com", "Admin");
                            MailAddress toadress = new MailAddress(CheckEmail, "User");
                            MailMessage message = new MailMessage(fromadress, toadress);
                            message.Body = "Введите этот код для восстановления пароля:" + Code;
                            message.Subject = "Восстановление пароля";

                            SmtpClient smtpClient = new SmtpClient();
                            smtpClient.Host = "smtp.gmail.com";
                            smtpClient.Port = 587;
                            smtpClient.EnableSsl = true;
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = new NetworkCredential(fromadress.Address, "Trophimchuk2002");

                            smtpClient.Send(message);
                        }

                     }
                    catch
                    {
                        return;
                    }
                    LoginForm.ChangePasswordGridB.Visibility = Visibility.Hidden;
                    wnd.ChangePassword.Visibility = Visibility.Visible;
                }
                );
            }
        }

        private RelayCommand passwordCheck;
        public RelayCommand PasswordCheck
        {
            get
            {
                return passwordCheck ?? new RelayCommand(obj =>
                {
                    LoginForm wnd = obj as LoginForm;
                    if (CheckPassword == Code)
                    {
                        wnd.ChangePassword.Visibility = Visibility.Hidden;
                        wnd.NewPassword.Visibility = Visibility.Visible;
                    }


                }
                );
            }
        }

        private RelayCommand change;
        public RelayCommand Change
        {
            get
            {
                return change ?? new RelayCommand(obj =>
                {
                    LoginForm wnd = obj as LoginForm;
                   
                    DataWorker.ChangeUserPassword(NewPassword, CheckEmail);
                    wnd.NewPassword.Visibility = Visibility.Hidden;
                    LoginForm.ChangePasswordGrid.Visibility = Visibility.Hidden;



                }
                );
            }
        }

       

        #endregion


        private RelayCommand checkUser;
        public RelayCommand CheckNewUser  
        {
            get
            {
                return checkUser ?? new RelayCommand(obj =>
                {
                    LoginForm wnd = obj as LoginForm;                  
                        if (DataWorker.AudintificateUser(Password, Email))
                        {
                         var rg = new Views.MainWindow();
                         rg.Owner = wnd;
                     
                         rg.Show();

                        Application.Current.MainWindow.Hide();
                        SetNullValuesToProperties();
                      
                        }
                    if (Email == "Admin" && Password == "123") 
                    {
                        Admin rg = new Admin();
                        rg.Show();
                        Application.Current.MainWindow.Hide();
                        SetNullValuesToProperties();
                    }
                    else
                    {
                        SetRedBlockControll(wnd, "UserName");
                    }
                }
                );
            }
        }

        //private List<User> allusers = DataWorker.GetAllUsers();
        //public List<User> AllUsers
        //{
        //    get { return allusers; }
        //    set
        //    {
        //        allusers = value;
        //        NotifyPropertyChanged("AllUsers");
        //    }
        //}


        #endregion



        #region Управление окном

        /// Закрытие окна регистрации
        private RelayCommand Close_Click;
        public RelayCommand btnClose_Click
        {
            get
            {
                if (Close_Click == null)
                {
                    Close_Click = new RelayCommand(b =>
                    {
                        LoginForm form = b as LoginForm;
                        form.Close();

                    });
                }
                return Close_Click;
            }
        }

        /// Изменение размера окна регистрации
        private RelayCommand Restore_Click;
        public RelayCommand btnRestore_Click
        {
            get
            {
                if (Restore_Click == null)
                {
                    Restore_Click = new RelayCommand(b =>
                    {
                        LoginForm form = b as LoginForm;
                        if (form.WindowState == WindowState.Normal)
                            form.WindowState = WindowState.Maximized;
                        else
                            form.WindowState = WindowState.Normal;
                    });
                }
                return Restore_Click;
            }
        }
  
        /// Уменьшение окна регистрации
        private RelayCommand Minimize_Click;
        public RelayCommand btnMinimize_Click
        {
            get
            {
                if (Minimize_Click == null)
                {
                    Minimize_Click = new RelayCommand(b =>
                    {
                        LoginForm form = b as LoginForm;
                        form.WindowState = WindowState.Minimized;
                    });
                }

                return Minimize_Click;
            }
        }
        #endregion 
    }
}
