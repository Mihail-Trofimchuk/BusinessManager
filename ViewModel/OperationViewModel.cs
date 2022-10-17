
using BusinessManager.Model;
using BusinessManager.Model.Data;
using BusinessManager.Views.Pages;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using RelayCommand = BusinessManager.Model.RelayCommand;

namespace BusinessManager.ViewModel
{
    public  class OperationViewModel : ViewModelBase
    {

     
    public OperationViewModel()
        {

            DataWorker.AddEntranceArticles();
            DataWorker.AddPaymentArticles();
            EntranceArticles = DataWorker.GetAllEntranceArticle();
            PaymentArticles = DataWorker.GetAllPaymentArticle();
        }

        public ObservableCollection<Article> EntranceArticles { get; set; } = DataWorker.GetAllEntranceArticle(); 
        public ObservableCollection<Article> PaymentArticles { get; set; } = DataWorker.GetAllPaymentArticle();  
        
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[A-Za-zA-я<>%$?!&_/^" + "\"" + "*@#()+=:;'\\s]");
            e.Handled = regex.IsMatch(e.Text);
        }


 

        #region Действия с окном
        private RelayCommand btn_createoperation;
        public RelayCommand Btn_createoperation
        {
            get
            {
                return btn_createoperation ?? new RelayCommand(obj =>
                {
                    Performbtn_createoperation();
                }
                );
            }
        }
        private void Performbtn_createoperation()
        {
            if (HomePage.OperationGrid.Visibility == Visibility.Hidden)
            {
                HomePage.OperationGrid.Visibility = Visibility.Visible;            
            }
            else
            {
                HomePage.OperationGrid.Visibility = Visibility.Hidden;
            }
            //HomePage.OperationFrame.Navigate(new System.Uri("Views/OperationMenu.xaml", UriKind.RelativeOrAbsolute));
        }

        private RelayCommand closeOperationMenu;
        public RelayCommand btncloseOperationMenu
        {
            get
            {
                return closeOperationMenu ?? new RelayCommand(obj =>
                {
                    PageAction();
                }
                );
            }
        }


       
        

        private RelayCommand entrancMenu;
        public RelayCommand EntrancMenu
        {
            get
            {
                return entrancMenu ?? new RelayCommand(obj =>
                {
                    HomePage page = obj as HomePage;
                   
                    if (page.OperationMenuNavEntrance.Visibility == Visibility.Hidden)
                    {
                        page.OperationMenuNavEntrance.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        page.OperationMenuNavPayment.Visibility = Visibility.Hidden;
                        page.OperationMenuNavEntrance.Visibility = Visibility.Visible;
                    }
                }
                );
            }
        }

        private RelayCommand paymentMenu;
        public RelayCommand PaymentMenu
        {
            get
            {
                return paymentMenu ?? new RelayCommand(obj =>
                {
                    HomePage page = obj as HomePage;
               
                    if (page.OperationMenuNavPayment.Visibility == Visibility.Hidden)
                    {
                        page.OperationMenuNavPayment.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        page.OperationMenuNavEntrance.Visibility = Visibility.Hidden;
                        page.OperationMenuNavPayment.Visibility = Visibility.Visible;
                    }
                }
                );
            }
        }


        private void PageAction()
        {
            if (HomePage.OperationGrid.Visibility == Visibility.Hidden)
            {
                HomePage.OperationGrid.Visibility = Visibility.Visible;
            }
            else
            {
                HomePage.OperationGrid.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        #region Свойства окна операций

        
        #region ПОСТУПЛЕНИЯ
        // Дата посступления

        private Nullable<DateTime> entrance_Day = null;
        public Nullable<DateTime> Entrance_Day
        {
            get
            {
                if (entrance_Day == null)
                {
                    entrance_Day = DateTime.Today;
                }
                return entrance_Day;
            }
            set
            {
                entrance_Day = value;
                RaisePropertyChanged("Entrance_Day");
            }
        }

        //  Все счета по айди пользователя
        private ObservableCollection<Account> _allaccountById = DataWorker.GetAllAccountById();
        public ObservableCollection<Account> AllAccountById
        {
            get { return _allaccountById; }
            set
            {
                _allaccountById = value;
                RaisePropertyChanged("AllAccountById");

            }
        }

        // Свойство для выбранного счета операции
        public static Account operationaccountentrance { get; set; }
        public Account OperationAccountEntrance
        {
            get { return operationaccountentrance; }
            set
            {
                operationaccountentrance = value;
                RaisePropertyChanged("OperationAccountEntrance");
            }
        }

        // Сумма поступления
        public static decimal? entrance_Sum { get; set; }
        public decimal? Entrance_Sum
        {
            get { return entrance_Sum; }
            set
            {
                entrance_Sum = value;
                RaisePropertyChanged("Entrance_Sum");
            }
        }

        //  Все контрагенты по айди пользователя
        private ObservableCollection<Сounterparty> _allcounterpartyById = DataWorker.GetAllСounterpartyById();
        public ObservableCollection<Сounterparty> AllCounterpartyById
        {
            get { return _allcounterpartyById; }
            set
            {
                _allcounterpartyById = value;
                RaisePropertyChanged("AllCounterpartyById");

            }
        }

        // Свойство для выбранного контагента операции
        public static Сounterparty operationcounterpartyentrance { get; set; }
        public Сounterparty OperationCounterpartyEntrance
        {
            get { return operationcounterpartyentrance; }
            set
            {
                operationcounterpartyentrance = value;
                RaisePropertyChanged("OperationCounterpartyEntrance");
            }
        }

      
        public static Article entranceArticle { get; set; }
        public Article EntranceArticle
        {
            get { return entranceArticle; }
            set
            {
                entranceArticle = value;
                RaisePropertyChanged("EntranceArticle");
            }
        }

        //  Все проекты по айди пользователя
        private ObservableCollection<Projects> _allProjectsById = DataWorker.GetAllProjectsById();
        public ObservableCollection<Projects> AllProjectsById
        {
            get { return _allProjectsById; }
            set
            {
                _allProjectsById = value;
                RaisePropertyChanged("AllProjectsById");

            }
        }

        // Свойство для выбранного проекта операции
        public static Projects operationProjectEntrance { get; set; }
        public Projects OperationProjectEntrance
        {
            get { return operationProjectEntrance; }
            set
            {
                operationProjectEntrance = value;
                RaisePropertyChanged("OperationProjectEntrance");
            }
        }

        // Комментарий поступления
        public static string entrance_Comment { get; set; }
        public string Entrance_Comment
        {
            get { return entrance_Comment; }
            set
            {
                entrance_Comment = value;
                RaisePropertyChanged("Entrance_Comment");
            }
        }

        //  Все проекты по айди пользователя
        private ObservableCollection<Entrance> _allEntranceById = DataWorker.GetAllEntranceById();
        public ObservableCollection<Entrance> AllEntranceById
        {
            get { return _allEntranceById; }
            set
            {
                _allEntranceById = value;
                RaisePropertyChanged("AllEntranceById");

            }
        }
        #endregion

        // Выплаты
        //  Все проекты по айди пользователя
        private ObservableCollection<Payment> _allPaymentById = DataWorker.GetAllPaymentById();
        public ObservableCollection<Payment> AllPaymentById
        {
            get { return _allPaymentById; }
            set
            {
                _allPaymentById = value;
                RaisePropertyChanged("AllPaymentById");

            }
        }
       
        // Свойство для выбранного счета операции выплаты
        public static Account operationAccountPayment { get; set; }
        public Account OperationAccountPayment
        {
            get { return operationAccountPayment; }
            set
            {
                operationAccountPayment = value;
                RaisePropertyChanged("OperationAccountPayment");
            }
        }

    

        // Дата выплаты
        private Nullable<DateTime> payment_Day = null;
        public Nullable<DateTime> Payment_Day
        {
            get
            {
                if (payment_Day == null)
                {
                    payment_Day = DateTime.Today;
                }
                return payment_Day;
            }
            set
            {
                payment_Day = value;
                RaisePropertyChanged("Payment_Day");
            }
        }

        // Cумма выплаты
        public static decimal? payment_Sum { get; set; }
        public decimal? Payment_Sum
        {
            get { return payment_Sum; }
            set
            {
                payment_Sum = value;
                RaisePropertyChanged("Payment_Sum");
            }
        }

        // Свойство для выбранного контагента операции выплаты
        public static Сounterparty operationCounterpartyPayment { get; set; }
        public Сounterparty OperationCounterpartyPayment
        {
            get { return operationCounterpartyPayment; }
            set
            {
                operationCounterpartyPayment  = value;
                RaisePropertyChanged("OperationCounterpartyPayment");
            }
        }

        // Статья операции
        public static Article paymentArticle { get; set; }
        public Article PaymentArticle
        {
            get { return paymentArticle; }
            set
            {
                paymentArticle = value;
                RaisePropertyChanged("PaymentArticle");
            }
        }

        // Свойство для выбранного проекта операции
        public static Projects operationProjectPayment { get; set; }
        public Projects OperationProjectPayment
        {
            get { return operationProjectPayment; }
            set
            {
                operationProjectPayment = value;
                RaisePropertyChanged("OperationProjectPayment");
            }
        }

        // Комментарий выплаты
        public static string payment_Comment { get; set; }
        public string Payment_Comment
        {
            get { return payment_Comment; }
            set
            {
                payment_Comment = value;
                RaisePropertyChanged("Payment_Comment");
            }
        }


        #endregion

        #region Действия с операциями

       

        private RelayCommand closeModal;
        public RelayCommand CloseModal
        {
            get
            {
                return closeModal ?? new RelayCommand(obj =>
                {
                    HomePage page = obj as HomePage;
                    page.ModelBorder.Visibility = Visibility.Hidden;
                    page.OperationModal.Visibility = Visibility.Hidden;
                });
            }
        }
        // Добавление поступления
        private RelayCommand addNewEntrance;
        public RelayCommand AddNewEntrance
        {
            get
            {
                return addNewEntrance ?? new RelayCommand(obj =>
                {
                    HomePage page = obj as HomePage;
                    string resultStr = "";
                    if (EntranceArticle == null || OperationAccountEntrance == null || OperationProjectEntrance == null || OperationCounterpartyEntrance == null)
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
                        page.ModelBorder.Visibility = Visibility.Visible;
                        page.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                        page.OperationModal.Visibility = Visibility.Visible;
                        return;
                    }
                    else
                    {
                        resultStr = DataWorker.CreatEntrance(Entrance_Day, OperationAccountEntrance, Entrance_Sum, EntranceArticle.ArticleTitle, OperationProjectEntrance, OperationCounterpartyEntrance, Entrance_Comment, SaveUser.CurrentUser);
                        AllEntranceById = DataWorker.GetAllEntranceById();
                        HomePage.Entrance.ItemsSource = AllEntranceById;
                        HomePage.Entrance.Items.Refresh();

                        AllAccountById = DataWorker.GetAllAccountById();
                        PaymentPage.AccountList.ItemsSource = AllAccountById;
                        PaymentPage.AccountList.Items.Refresh();

                        SetNullValuesToProperties();
                        PageAction();
                        CurrentPage.AllEntrance = null;
                        CurrentPage.AllEntrance = AllEntranceById;

                        NotesPage.PieSer.ItemsSource = AllEntranceById;
                        NotesPage.PieSer.Items.Refresh();

                        HomePage.EntranceCount.Text = AllEntranceById.Count.ToString();
                        NotesPage.ToTalEntr.Text = DataWorker.GetTotalEntranceById().ToString() + '$';
                        NotesPage.Ranges.Text = DataWorker.GetRangeById().ToString() + '$';
                        NotesPage.Dev.Text = DataWorker.GetDevedentsId().ToString() + '$';
                    }
                }
                );
            }
        }

        // Добавление выплаты
        private RelayCommand addNewPayment;
        public RelayCommand AddNewPayment
        {
            get
            {
                return addNewPayment ?? new RelayCommand(obj =>
                {
                    HomePage page = obj as HomePage;
                    string resultStr = "";
                    if (OperationAccountPayment == null || PaymentArticle == null || OperationProjectPayment == null || OperationCounterpartyPayment == null)
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
                        page.ModelBorder.Visibility = Visibility.Visible;
                        page.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                        page.OperationModal.Visibility = Visibility.Visible;
                        return;
                    }
                    else
                    {
                        resultStr = DataWorker.CreatePayment(Payment_Day, OperationAccountPayment, Payment_Sum, PaymentArticle.ArticleTitle, OperationProjectPayment, OperationCounterpartyPayment, Payment_Comment, SaveUser.CurrentUser);
                        AllPaymentById = DataWorker.GetAllPaymentById();
                        HomePage.Payment.ItemsSource = AllPaymentById;
                        HomePage.Payment.Items.Refresh();

                        AllAccountById = DataWorker.GetAllAccountById();
                        PaymentPage.AccountList.ItemsSource = AllAccountById;
                        PaymentPage.AccountList.Items.Refresh();
                        SetNullValuesToProperties();

                        PageAction();

                        CurrentPage.AllPayment = null;
                        CurrentPage.AllPayment = AllPaymentById;

                        NotesPage.PieSer2.ItemsSource = AllPaymentById;
                        NotesPage.PieSer2.Items.Refresh();
                        NotesPage.PieSer3.ItemsSource = AllPaymentById;
                        NotesPage.PieSer3.Items.Refresh();
                        HomePage.PaymentCount.Text = AllPaymentById.Count.ToString();
                        NotesPage.TotalPaym.Text = '-' +DataWorker.GetTotalPaymentById().ToString() + '$';
                        NotesPage.Ranges.Text = DataWorker.GetRangeById().ToString() + '$';
                        NotesPage.Dev.Text = DataWorker.GetDevedentsId().ToString() + '$';
                    }
                }
                );
            }
        }



        private void SetNullValuesToProperties()
        {
            Entrance_Day = null;
            OperationAccountEntrance = null;
            Entrance_Sum = null;
            EntranceArticle = null;
            OperationProjectEntrance = null;
            OperationCounterpartyEntrance = null;
            Entrance_Comment = null;

            Payment_Day = null;
            OperationAccountPayment = null;
            Payment_Sum = null;
            PaymentArticle = null;
            OperationProjectPayment = null;
            OperationCounterpartyPayment = null;
            Payment_Comment = null;


            Payment_DayEd = null;
            Payment_SumEd = null;
            Payment_CommentEd = null;
            OperationAccountPaymentEd = null;
            PaymentArticleEd = null;
            OperationProjectPaymentEd = null;
            OperationCounterpartyPaymentEd = null;
            Payment_CommentEd = null;

            Entrance_DayEd = null;
            Entrance_SumEd = null;
            Entrance_CommentEd = null;
            OperationAccountEntranceEd = null;
            EntranceArticleEd = null;
            OperationProjectEntranceEd = null;
            OperationCounterpartyEntranceEd = null;
            Entrance_CommentEd = null;

        }
        #endregion

        #region Удаление поступлений и выплат

        // Команада удаления поступления
        private RelayCommand deleteentrance;
        public RelayCommand DeleteEntrance
        {
            get
            {
                return deleteentrance ?? new RelayCommand(obj =>
                {
                    DataWorker.DeleteEntrance(SelectedEntrance);
                    AllEntranceById = DataWorker.GetAllEntranceById();

                    HomePage.Entrance.ItemsSource = null;
                    HomePage.Entrance.Items.Clear();
                    HomePage.Entrance.ItemsSource = AllEntranceById;
                    HomePage.Entrance.Items.Refresh();
                    HomePage.EntranceCount.Text = AllEntranceById.Count.ToString();
                    NotesPage.ToTalEntr.Text = DataWorker.GetTotalEntranceById().ToString() + '$';
                    NotesPage.Ranges.Text = DataWorker.GetRangeById().ToString() + '$';
                    NotesPage.Dev.Text = DataWorker.GetDevedentsId().ToString() + '$';
                }
                );
            }
        }

        // Команада удаления выплаты
        private RelayCommand deletepayment;
        public RelayCommand DeletePayment
        {
            get
            {
                return deletepayment ?? new RelayCommand(obj =>
                {
                    DataWorker.DeletePayment(SelectedPayment);
                    AllPaymentById = DataWorker.GetAllPaymentById();

                    HomePage.Payment.ItemsSource = null;
                    HomePage.Payment.Items.Clear();
                    HomePage.Payment.ItemsSource = AllPaymentById;
                    HomePage.Payment.Items.Refresh();
                    HomePage.PaymentCount.Text = AllPaymentById.Count.ToString();
                    NotesPage.TotalPaym.Text = '-' + DataWorker.GetTotalPaymentById().ToString() + '$';
                    NotesPage.Ranges.Text = DataWorker.GetRangeById().ToString() + '$';
                    NotesPage.Dev.Text = DataWorker.GetDevedentsId().ToString() + '$';
                }
                );
            }
        }

        private Entrance selectentrance;
        public Entrance SelectedEntrance
        {
            get { return selectentrance; }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectentrance = value;
                RaisePropertyChanged("SelectedEntrance");
            }
        }

        private Payment selectepayment;
        public Payment SelectedPayment
        {
            get { return selectepayment; }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectepayment = value;
                RaisePropertyChanged("SelectedPayment");
            }
        }

        #endregion

        #region Поиск

        public static string entranceSearch { get; set; }
        public string EntranceSearch
        {
            get { return entranceSearch; }
            set
            {
                entranceSearch  = value;
                AllEntranceById = DataWorker.GetFindEntranceById(value);
                HomePage.Entrance.ItemsSource = AllEntranceById;
                RaisePropertyChanged("EntranceSearch");
            }
        }

        public static string paymentSearch { get; set; }
        public string PaymentSearch
        {
            get { return paymentSearch; }
            set
            {
                paymentSearch = value;
                AllPaymentById = DataWorker.GetFindPaymentById(value);
                HomePage.Payment.ItemsSource = AllPaymentById;
                RaisePropertyChanged("PaymentSearch");
            }
        }

        // Свойство поиска поступления
        private static DateTime entranceDate = DateTime.Today;
        public DateTime EntranceDate
        {
            get
            {
                return entranceDate;
            }
            set
            {
                if (entranceDate == null)
                {
                    entranceDate = DateTime.Today;
                }
                entranceDate = value;
                AllEntranceById = DataWorker.GetFindEntranceByDateId(value);
                HomePage.Entrance.ItemsSource = AllEntranceById;
                RaisePropertyChanged("EntranceDate");

            }
        }

        // Свойство поиска выплаты
        private static DateTime paymenteDate = DateTime.Today;
        public DateTime PaymenteDate
        {
            get
            {
                return paymenteDate;
            }
            set
            {
                if (paymenteDate == null)
                {
                    paymenteDate = DateTime.Today;
                }
                paymenteDate = value;
                AllPaymentById = DataWorker.GetFindPaymentByDateId(value);
                HomePage.Payment.ItemsSource = AllPaymentById;
                RaisePropertyChanged("PaymenteDate");

            }
        }

        #endregion

        #region  Редактирование

        // Свойство для выбранного счета операции выплаты
        public static Account operationAccountPaymentEd { get; set; }
        public Account OperationAccountPaymentEd
        {
            get { return operationAccountPaymentEd; }
            set
            {
                operationAccountPaymentEd = value;
                RaisePropertyChanged("OperationAccountPaymentEd");
            }
        }



        // Дата выплаты
        private Nullable<DateTime> payment_DayEd = null;
        public Nullable<DateTime> Payment_DayEd
        {
            get
            {
                if (payment_DayEd == null)
                {
                    payment_DayEd = DateTime.Today;
                }
                return payment_DayEd;
            }
            set
            {
                payment_DayEd = value;
                RaisePropertyChanged("Payment_DayEd");
            }
        }

        // Cумма выплаты
        public static decimal? payment_SumEd { get; set; }
        public decimal? Payment_SumEd
        {
            get { return payment_SumEd; }
            set
            {
                payment_SumEd = value;
                RaisePropertyChanged("Payment_SumEd");
            }
        }

        // Свойство для выбранного контагента операции выплаты
        public static Сounterparty operationCounterpartyPaymentEd { get; set; }
        public Сounterparty OperationCounterpartyPaymentEd
        {
            get { return operationCounterpartyPaymentEd; }
            set
            {
                operationCounterpartyPaymentEd = value;
                RaisePropertyChanged("OperationCounterpartyPaymentEd");
            }
        }

        // Статья операции
        public static Article paymentArticleEd { get; set; }
        public Article PaymentArticleEd
        {
            get { return paymentArticleEd; }
            set
            {
                paymentArticleEd = value;
                RaisePropertyChanged("PaymentArticleEd");
            }
        }

        // Свойство для выбранного проекта операции
        public static Projects operationProjectPaymentEd { get; set; }
        public Projects OperationProjectPaymentEd
        {
            get { return operationProjectPaymentEd; }
            set
            {
                operationProjectPaymentEd = value;
                RaisePropertyChanged("OperationProjectPaymentEd");
            }
        }

        // Комментарий выплаты
        public static string payment_CommentEd { get; set; }
        public string Payment_CommentEd
        {
            get { return payment_CommentEd; }
            set
            {
                payment_CommentEd = value;
                RaisePropertyChanged("Payment_CommentEd");
            }
        }

        // OperationEditGrid;
        //EntranceEditGrid;
        // PaymentEditGrid;

        // Изменение контрагента
        private RelayCommand changePayment;
        public RelayCommand ChangePayment
        {
            get
            {
                return changePayment ?? new RelayCommand(obj =>
                {
                    HomePage.OperationEditGrid.Visibility = Visibility.Visible;
                    HomePage.PaymentEditGrid.Visibility = Visibility.Visible;
                   
                    Payment_DayEd = SelectedPayment.Payment_Day;
                    Payment_SumEd = SelectedPayment.Payment_Sum;
                    Payment_CommentEd = SelectedPayment.PaymentPurpose;

                });
            }
        }

        private RelayCommand changeNewPayment;
        public RelayCommand ChangeNewPayment
        {
            get
            {
                return changeNewPayment ?? new RelayCommand(obj =>
                {
                    HomePage page = obj as HomePage;
                    string resultStr = "";

                    if (PaymentArticleEd == null || OperationCounterpartyPaymentEd == null || OperationProjectPaymentEd == null || OperationAccountPaymentEd == null)
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
                        page.ModelBorder.Visibility = Visibility.Visible;
                        page.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                        page.OperationModal.Visibility = Visibility.Visible;

                    
                     
                        return;
                    }
                    else
                    {
                        resultStr = DataWorker.EditPayment(SelectedPayment, Payment_DayEd, OperationAccountPaymentEd, Payment_SumEd, PaymentArticleEd, OperationProjectPaymentEd, OperationCounterpartyPaymentEd, Payment_CommentEd);
                        AllPaymentById = DataWorker.GetAllPaymentById();
                        HomePage.Payment.ItemsSource = null;
                        HomePage.Payment.Items.Clear();
                        HomePage.Payment.ItemsSource = AllPaymentById;
                        HomePage.Payment.Items.Refresh();


                        HomePage.OperationEditGrid.Visibility = Visibility.Hidden;
                        HomePage.PaymentEditGrid.Visibility = Visibility.Hidden;

                        NotesPage.PieSer2.ItemsSource = AllPaymentById;
                        NotesPage.PieSer2.Items.Refresh();
                        NotesPage.PieSer3.ItemsSource = AllPaymentById;
                        NotesPage.PieSer3.Items.Refresh();
                        HomePage.PaymentCount.Text = AllPaymentById.Count.ToString();
                        NotesPage.TotalPaym.Text = '-' + DataWorker.GetTotalPaymentById().ToString() + '$';
                        NotesPage.Ranges.Text = DataWorker.GetRangeById().ToString() + '$';
                        NotesPage.Dev.Text = DataWorker.GetDevedentsId().ToString() + '$';

                        SetNullValuesToProperties();
                    }


                }
                );
            }
        }

        private RelayCommand closeEditPayment;
        public RelayCommand CloseEditPayment
        {
            get
            {
                return closeEditPayment ?? new RelayCommand(obj =>
                {

                    HomePage.OperationEditGrid.Visibility = Visibility.Hidden;
                    HomePage.PaymentEditGrid.Visibility = Visibility.Hidden;

                });
            }
        }




        //  Редактирование поступления


        private Nullable<DateTime> entrance_DayEd = null;
        public Nullable<DateTime> Entrance_DayEd
        {
            get
            {
                if (entrance_DayEd == null)
                {
                    entrance_DayEd = DateTime.Today;
                }
                return entrance_DayEd;
            }
            set
            {
                entrance_Day = value;
                RaisePropertyChanged("Entrance_DayEd");
            }
        }


        // Свойство для выбранного счета операции
        public static Account operationaccountentranceEd { get; set; }
        public Account OperationAccountEntranceEd
        {
            get { return operationaccountentranceEd; }
            set
            {
                operationaccountentranceEd = value;
                RaisePropertyChanged("OperationAccountEntranceEd");
            }
        }

        // Сумма поступления
        public static decimal? entrance_SumEd { get; set; }
        public decimal? Entrance_SumEd
        {
            get { return entrance_SumEd; }
            set
            {
                entrance_SumEd = value;
                RaisePropertyChanged("Entrance_SumEd");
            }
        }


        // Свойство для выбранного контагента операции
        public static Сounterparty operationcounterpartyentranceEd { get; set; }
        public Сounterparty OperationCounterpartyEntranceEd
        {
            get { return operationcounterpartyentranceEd; }
            set
            {
                operationcounterpartyentranceEd = value;
                RaisePropertyChanged("OperationCounterpartyEntranceEd");
            }
        }


        public static Article entranceArticleEd { get; set; }
        public Article EntranceArticleEd
        {
            get { return entranceArticleEd; }
            set
            {
                entranceArticleEd = value;
                RaisePropertyChanged("EntranceArticleEd");
            }
        }


        // Свойство для выбранного проекта операции
        public static Projects operationProjectEntranceEd { get; set; }
        public Projects OperationProjectEntranceEd
        {
            get { return operationProjectEntranceEd; }
            set
            {
                operationProjectEntranceEd = value;
                RaisePropertyChanged("OperationProjectEntranceEd");
            }
        }

        // Комментарий поступления
        public static string entrance_CommentEd { get; set; }
        public string Entrance_CommentEd
        {
            get { return entrance_CommentEd; }
            set
            {
                entrance_CommentEd = value;
                RaisePropertyChanged("Entrance_CommentEd");
            }
        }


   
        // Изменение контрагента
        private RelayCommand changeEntrance;
        public RelayCommand ChangeEntrance
        {
            get
            {
                return changeEntrance ?? new RelayCommand(obj =>
                {
                    HomePage.OperationEditGrid.Visibility = Visibility.Visible;
                    HomePage.EntranceEditGrid.Visibility = Visibility.Visible;

                    Entrance_DayEd = SelectedEntrance.Entrance_Day;
                    Entrance_SumEd = SelectedEntrance.Entrance_Sum;
                    Entrance_CommentEd = SelectedEntrance.EntrancePurpose;

                });
            }
        }

        private RelayCommand changeNewEntrance;
        public RelayCommand ChangeNewEntrance
        {
            get
            {
                return changeNewEntrance ?? new RelayCommand(obj =>
                {
                    HomePage page = obj as HomePage;
                    string resultStr = "";

                    if (EntranceArticleEd == null || OperationCounterpartyEntranceEd == null || OperationProjectEntranceEd == null || OperationAccountEntranceEd == null)
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
                        page.ModelBorder.Visibility = Visibility.Visible;
                        page.ModelBorder.BeginAnimation(ContentControl.WidthProperty, animtion);
                        page.OperationModal.Visibility = Visibility.Visible;



                        return;
                    }
                    else
                    {
                        resultStr = DataWorker.EditEntrance(SelectedEntrance, Entrance_DayEd, OperationAccountEntranceEd, Entrance_SumEd, EntranceArticleEd, OperationProjectEntranceEd, OperationCounterpartyEntranceEd, Entrance_CommentEd);
                        AllEntranceById = DataWorker.GetAllEntranceById();
                        HomePage.Entrance.ItemsSource = null;
                        HomePage.Entrance.Items.Clear();
                        HomePage.Entrance.ItemsSource = AllEntranceById;
                        HomePage.Entrance.Items.Refresh();

                        HomePage.OperationEditGrid.Visibility = Visibility.Hidden;
                        HomePage.EntranceEditGrid.Visibility = Visibility.Hidden;

                        NotesPage.PieSer.ItemsSource = AllEntranceById;
                        NotesPage.PieSer.Items.Refresh();

                        HomePage.EntranceCount.Text = AllEntranceById.Count.ToString();
                        NotesPage.ToTalEntr.Text = DataWorker.GetTotalEntranceById().ToString() + '$';
                        NotesPage.Ranges.Text = DataWorker.GetRangeById().ToString() + '$';
                        NotesPage.Dev.Text = DataWorker.GetDevedentsId().ToString() + '$';

                        SetNullValuesToProperties();
                    }


                }
                );
            }
        }

        private RelayCommand closeEditEntrance;
        public RelayCommand CloseEditEntrance
        {
            get
            {
                return closeEditEntrance ?? new RelayCommand(obj =>
                {

                    HomePage.OperationEditGrid.Visibility = Visibility.Hidden;
                    HomePage.EntranceEditGrid.Visibility = Visibility.Hidden;

                });
            }
        }
        #endregion
    }

}

