using BusinessManager.Model;
using BusinessManager.Model.Data;
using BusinessManager.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BusinessManager.ViewModel
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        public AccountViewModel()
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

        #region Действия с окном
        private RelayCommand openAccountMenu;
        public RelayCommand OpenAccountMenu
        {
            get
            {
                return openAccountMenu ?? new RelayCommand(obj =>
                {
                    CurrentPage.PaymentPage = obj as PaymentPage;
                    if (PaymentPage.AccountGrid.Visibility == Visibility.Hidden)
                    {
                        PaymentPage.AccountGrid.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        PaymentPage.AccountGrid.Visibility = Visibility.Hidden;
                    }

                   // PaymentPage.AccountFrame.Navigate(new System.Uri("Views/AccountMenu.xaml", UriKind.RelativeOrAbsolute));

                    //var w = Application.Current.MainWindow.DataContext as MainApplicationViewModel;
                    

                    //CurrentPage.AllLegalEntityByIdTry = DataWorker.GetAllLegalEntityById();
                    //AllLegalEntityById = DataWorker.GetAllLegalEntityById();

                   // AllLegalEntityById = CurrentPage.AllLegalEntityByIdTry;

                   // RaisePropertyChanged("AllLegalEntityById");
                }
                );
            }
        }

        private RelayCommand closeAccountMenu;
        public RelayCommand btncloseAccountMenu
        {
            get
            {
                return closeAccountMenu ?? new RelayCommand(obj =>
                {
                    PageAction();
                }
                );
            }
        }

        private void PageAction()
        {
            if (PaymentPage.AccountGrid.Visibility == Visibility.Hidden)
            {
                PaymentPage.AccountGrid.Visibility = Visibility.Visible;
            }
            else
            {
                PaymentPage.AccountGrid.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        #region Свойства счетов

        private ObservableCollection<Account> _allaccountsById = DataWorker.GetAllAccountById();
        public ObservableCollection<Account> AllaccountsById
        {
            get { return _allaccountsById; }
            set
            {
                _allaccountsById = value;
                RaisePropertyChanged("AllaccountsById");

            }
        }

        //  Все юристы по айди пользователя
        private ObservableCollection<LegalEntity> _allLegalEntityById = DataWorker.GetAllLegalEntityById();
        public ObservableCollection<LegalEntity> AllLegalEntityById
        {
            get { return _allLegalEntityById; }
            set
            {
                _allLegalEntityById = value;
                RaisePropertyChanged("AllLegalEntityById");

            }
        }

        // Свойство для Счета
        public static LegalEntity accountLegalEntity { get; set; }
        public LegalEntity AccountLegalEntity
        {
            get { return accountLegalEntity; }
            set
            {
                accountLegalEntity = value;
                NotifyPropertyChanged("AccountLegalEntity");
            }
        }

        // Имя счета
        public static string account_Name { get; set; }
        public string Account_Name
        {
            get { return account_Name; }
            set
            {
                account_Name = value;
                NotifyPropertyChanged("Account_Name");
            }
        }

       

        // Тип счета
        public static ComboBoxItem account_Type { get; set; }
        public ComboBoxItem Account_Type
        {
            get { return account_Type; }
            set
            {        
                account_Type = value;
                NotifyPropertyChanged("Account_Type");
            }
        }

        // Начальный остаток
        public static decimal? account_FirstInput { get; set; }
        public decimal? Account_FirstInput
        {
            get { return account_FirstInput; }
            set
            {
                account_FirstInput = value;
                NotifyPropertyChanged("Account_FirstInput");
            }
        }

        // Дата внесения счета
        public static DateTime? account_DateInput = null;
        public DateTime? Account_DateInput
        {
            get {

                if (account_DateInput == null)
                {
                    account_DateInput = DateTime.Today;
                }
                return account_DateInput;
            }
            set
            {
                account_DateInput = value;
                NotifyPropertyChanged("Account_DateInput");
            }
        }

  

        // Комментарий счета
        public static string account_Comment { get; set; }
        public string Account_Comment
        {
            get { return account_Comment; }
            set
            {
                account_Comment = value;
                NotifyPropertyChanged("Account_Comment");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected virtual bool SetProperty<T>(ref T member, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(member, value))
            {
                return false;
            }

            member = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private string? _selectedValidationOutlined;

        public string? SelectedValidationOutlined
        {
            get => _selectedValidationOutlined;
            set => SetProperty(ref _selectedValidationOutlined, value);
        }

        //public List<string> Type = new() { "Наличный","Бкзналичный","Карта физлица" };

        #endregion

        #region Работа с счетами

        private void SetRedBlockControll(Page wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        // Добавление счета  
        private RelayCommand addNewAccount;
        public RelayCommand AddNewAccount
        {
            get
            {
                return addNewAccount ?? new RelayCommand(obj =>
                {
                    PaymentPage page = obj as PaymentPage;

                    string resultStr = "";
                    if (Account_Type == null || AccountLegalEntity == null )
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

                        page.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                        page.AccountModal.Visibility = Visibility.Visible;

                        //AccountModalWindow window = new AccountModalWindow();
                        //window.Show();
                        return;
                    }
                    else
                    {
                        resultStr = DataWorker.CreatAccount(Account_Name, AccountLegalEntity, Account_Type.Content.ToString(), Account_FirstInput, Account_Comment, Account_DateInput, SaveUser.CurrentUser);
                        SetWindow(CurrentPage.Сounterparty);
                        SetNullValuesToProperties();
                        PageAction();
                        HomePage.Account.ItemsSource = AllaccountsById;
                        HomePage.AccountP.ItemsSource = AllaccountsById;
                        //MainWindow.Total.Text = DataWorker.GetFindUserTotalAccountById().ToString();
                        PaymentPage.AccountCount.Text = AllaccountsById.Count.ToString();

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
                    PaymentPage page = obj as PaymentPage;


                    //page.AccountModal.BeginAnimation(ContentControl.MarginProperty, animtion);
                    page.AccountModal.Visibility = Visibility.Hidden;

                });
            }
        }
        
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[A-Za-zA-я<>%$?!&_/^" + "\"" + "*@#()+=:;'\\s]");
            regex.Replace(" ", "");
            e.Handled = regex.IsMatch(e.Text);
        }

    


        private void SetWindow(Page wnd)
        {
            AllaccountsById = DataWorker.GetAllAccountById();
            PaymentPage.AccountList.ItemsSource = null;
            PaymentPage.AccountList.Items.Clear();
            PaymentPage.AccountList.ItemsSource = AllaccountsById;
            PaymentPage.AccountList.Items.Refresh();
        }
        private void SetNullValuesToProperties()
        {
            Account_Name = null;
            AccountLegalEntity = null;
            Account_Type = null;
            Account_FirstInput = 0;
            Account_Comment = null;
            Account_NameСh = null;         
            Account_FirstInputCh = null;
            Account_DateInputCh = null;
            Account_CommentCh = null;
            AccountLegalEntityCh = null;
            Account_TypeCh = null;
        }

        #endregion

        #region Поиск

        // Свойство поиска контрагента
        private static string accountName { get; set; }
        public string AccountName
        {
            get { return accountName; }
            set
            {
                accountName = value;
                AllaccountsById = DataWorker.GetFindAccountById(value);
                PaymentPage.AccountList.ItemsSource = AllaccountsById;
                NotifyPropertyChanged("LegalEntityName");

            }
        }

        private static bool nType { get; set; }
        public bool NType
        {
            get { return nType; }
            set
            {
                nType = value;
                NotifyPropertyChanged("NType");

            }
        }

        private static bool bType { get; set; }
        public bool BType
        {
            get { return bType; }
            set
            {
                bType = value;
                NotifyPropertyChanged("BType");

            }
        }
        private static bool cType { get; set; }
        public bool CType
        {
            get { return cType; }
            set
            {
                cType = value;
                NotifyPropertyChanged("CType");

            }
        }

        private static bool eType { get; set; }
        public bool EType
        {
            get { return eType; }
            set
            {
                eType = value;
                NotifyPropertyChanged("EType");

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
                    if (NType == true)
                    {
                        AllaccountsById = DataWorker.GetFindAccountByNTypeId("Наличный");
                        PaymentPage.AccountList.ItemsSource = AllaccountsById;
                    }
                    if (BType == true)
                    {
                        AllaccountsById = DataWorker.GetFindAccountByNTypeId("Безналичный");
                        PaymentPage.AccountList.ItemsSource = AllaccountsById;
                    }
                    if (CType == true)
                    {
                        AllaccountsById = DataWorker.GetFindAccountByNTypeId("Карта физлица");
                        PaymentPage.AccountList.ItemsSource = AllaccountsById;
                    }
                     if (EType == true)
                    {
                        AllaccountsById = DataWorker.GetFindAccountByNTypeId("Электронный");
                        PaymentPage.AccountList.ItemsSource = AllaccountsById;
                    }
                    if (AllType == true)
                    {
                        AllaccountsById = DataWorker.GetAllAccountById();
                        PaymentPage.AccountList.ItemsSource = AllaccountsById;
                    }

                }
                );
            }
        }

        #endregion

        // Команада удаления счета
        private RelayCommand deleteAccount;
        public RelayCommand DeleteAccount
        {
            get
            {
                return deleteAccount ?? new RelayCommand(obj => {

                    DataWorker.DeleteAccount(SelectedAccount);
                    AllaccountsById = DataWorker.GetAllAccountById();
                    PaymentPage.AccountList.ItemsSource = null;
                    PaymentPage.AccountList.Items.Clear();
                    PaymentPage.AccountList.ItemsSource = AllaccountsById;
                    PaymentPage.AccountList.Items.Refresh();
                    PaymentPage.AccountCount.Text = AllaccountsById.Count.ToString();
                    HomePage.Account.ItemsSource = AllaccountsById;
                    HomePage.AccountP.ItemsSource = AllaccountsById;

                }
                );
            }
        }

        private Account selectaccount;
        public Account SelectedAccount
        {
            get { return selectaccount; }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectaccount = value;
                NotifyPropertyChanged("SelectedAccount");
            }
        }

        #region  Редактирование счета 

        private static string account_NameСh { get; set; }

        public string Account_NameСh
        {
            get { return account_NameСh; }
            set
            {
                account_NameСh = value;
                NotifyPropertyChanged("Account_NameСh");

            }
        }

        public static LegalEntity accountLegalEntityCh { get; set; }
        public LegalEntity AccountLegalEntityCh
        {
            get { return accountLegalEntityCh; }
            set
            {
                accountLegalEntityCh = value;
                NotifyPropertyChanged("AccountLegalEntityCh");
            }
        }

   



        // Тип счета
        public static ComboBoxItem account_TypeCh { get; set; }
        public ComboBoxItem Account_TypeCh
        {
            get { return account_TypeCh; }
            set
            {
                account_TypeCh = value;
                NotifyPropertyChanged("Account_TypeCh");
            }
        }

        // Начальный остаток
        public static decimal? account_FirstInputCh { get; set; }
        public decimal? Account_FirstInputCh
        {
            get { return account_FirstInputCh; }
            set
            {
                account_FirstInputCh = value;
                NotifyPropertyChanged("Account_FirstInputCh");
            }
        }

        // Дата внесения счета
        public static DateTime? account_DateInputCh = null;
        public DateTime? Account_DateInputCh
        {
            get
            {

                if (account_DateInputCh == null)
                {
                    account_DateInputCh = DateTime.Today;
                }
                return account_DateInputCh;
            }
            set
            {
                account_DateInputCh = value;
                NotifyPropertyChanged("Account_DateInputCh");
            }
        }



        // Комментарий счета
        public static string account_CommentCh { get; set; }
        public string Account_CommentCh
        {
            get { return account_CommentCh; }
            set
            {
                account_CommentCh = value;
                NotifyPropertyChanged("Account_CommentCh");
            }
        }


        // Изменение контрагента
        private RelayCommand changeCounterparty;
        public RelayCommand ChangeCounterparty
        {
            get
            {
                return changeCounterparty ?? new RelayCommand(obj =>
                {
                    PaymentPage.AccountGrid.Visibility = Visibility.Visible;
                    PaymentPage.AccountBorder.Visibility = Visibility.Visible;

                    
                    Account_NameСh = SelectedAccount.Account_Name;            
                    Account_FirstInputCh = SelectedAccount.CurrentBalance;
                    Account_DateInputCh = SelectedAccount.AccountDateImput;
                    Account_CommentCh = SelectedAccount.Account_Comment;
                });
            }
        }

        private RelayCommand changeNewCounterParty;
        public RelayCommand ChangeNewCounterParty
        {
            get
            {
                return changeNewCounterParty ?? new RelayCommand(obj =>
                {
                PaymentPage page = obj as PaymentPage;
                    string resultStr = "";

                    if (Account_TypeCh == null || AccountLegalEntityCh == null)
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

                        page.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                        page.AccountModal.Visibility = Visibility.Visible;

                        //AccountModalWindow window = new AccountModalWindow();
                        //window.Show();
                        return;
                    }
                    else
                    {
                        resultStr = DataWorker.EditAccount(SelectedAccount, Account_NameСh, AccountLegalEntityCh, Account_TypeCh.Content.ToString(), Account_FirstInputCh, Account_CommentCh);
                        AllaccountsById = DataWorker.GetAllAccountById();
                        PaymentPage.AccountList.ItemsSource = null;
                        PaymentPage.AccountList.Items.Clear();
                        PaymentPage.AccountList.ItemsSource = AllaccountsById;
                        PaymentPage.AccountList.Items.Refresh();

                        HomePage.Account.ItemsSource = AllaccountsById;
                        HomePage.AccountP.ItemsSource = AllaccountsById;

                        PaymentPage.AccountGrid.Visibility = Visibility.Hidden;
                        PaymentPage.AccountBorder.Visibility = Visibility.Hidden;

                        SetNullValuesToProperties();
                    }

         
                }
                );
            }
        }

        private RelayCommand closeEditAccount;
        public RelayCommand CloseEditAccount
        {
            get
            {
                return closeEditAccount ?? new RelayCommand(obj =>
                {

                    PaymentPage.AccountGrid.Visibility = Visibility.Hidden;
                    PaymentPage.AccountBorder.Visibility = Visibility.Hidden;

                });
            }
        }

        #endregion

    }
}
