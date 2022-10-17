using BusinessManager.Model;
using BusinessManager.Model.Data;
using BusinessManager.Views.Pages;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace BusinessManager.ViewModel
{
    public class CounterpartyViewModel : INotifyPropertyChanged
    {

        public CounterpartyViewModel()
        {
           // CountofLegalEntity_Name = Conterpaty.LegalEntityList.Items.Count;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        // Получени окна для поиска юр лица и контрагента
        private RelayCommand searchLeg;
        public RelayCommand SearchLegalEntity
        {
            get
            {
                return searchLeg ?? new RelayCommand(obj =>
                {
                    CurrentPage.Сounterparty = obj as Conterpaty;  
                }
                );
            }
        }

        // Команада удаления контрагента
        private RelayCommand deleteLegalEntity;
        public RelayCommand DeleteLegalEntity
        {
            get
            {
                return deleteLegalEntity ?? new RelayCommand(obj =>                {
          
                    DataWorker.DeleteLegalEntity(LegalEntityNa);
                    AllLegalEntityById = DataWorker.GetAllLegalEntityById();
                    Conterpaty.LegalEntityList.ItemsSource = null;
                    Conterpaty.LegalEntityList.Items.Clear();
                    Conterpaty.LegalEntityList.ItemsSource = AllLegalEntityById;
                    Conterpaty.LegalEntityList.Items.Refresh();
                    Conterpaty.CountofL.Text = AllLegalEntityById.Count.ToString();
                    PaymentPage.LegalEntityBox.ItemsSource = AllLegalEntityById;

                }
                );
            }
        }

        // Команада удаления контрагента
        private RelayCommand deleteCounterparty;
        public RelayCommand DeleteCounterparty
        {
            get
            {
                return deleteCounterparty ?? new RelayCommand(obj =>
                {
                    DataWorker.DeleteCounterparty(SelectCounterparty);
                    AllcounterPartiesById = DataWorker.GetAllСounterpartyById();
                    Conterpaty.CounterpartyList.ItemsSource = null;
                    Conterpaty.CounterpartyList.Items.Clear();
                    Conterpaty.CounterpartyList.ItemsSource = AllcounterPartiesById;
                    Conterpaty.CounterpartyList.Items.Refresh();
                    Conterpaty.CountofC.Text = AllcounterPartiesById.Count.ToString();
                    HomePage.Counterparty.ItemsSource = AllcounterPartiesById;
                    HomePage.CounterpartyP.ItemsSource = AllcounterPartiesById;
                }
                );
            }
        }


        private LegalEntity selectLegal;
        public LegalEntity LegalEntityNa
        {
            get { return selectLegal; }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectLegal = value;
                NotifyPropertyChanged("LegalEntityNa");
            }
        }

     

        private Сounterparty selectCounterparty;
        public Сounterparty SelectCounterparty
        {
            get { return selectCounterparty; }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectCounterparty = value;
                NotifyPropertyChanged("SelectCounterparty");
            }
        }

       

        #region Методы VW
        private void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Действия окна Counterparty
        private void PageAction()
        {
            if (Conterpaty.LegalEntity.Visibility == Visibility.Hidden)
            {
                Conterpaty.LegalEntity.Visibility = Visibility.Visible;
            }
            else
            {
                Conterpaty.LegalEntity.Visibility = Visibility.Hidden;
            }
        }

        // Закрытие окна добавления 
        private RelayCommand closeCounterPartyMenu;
        public RelayCommand btncloseCOunterPartyMenu
        {
            get
            {
                return closeCounterPartyMenu ?? new RelayCommand(obj =>
                {
                    PageAction();
                }
                );
            }
        }

        // Открытие окна добавдение Юр Лица
        private RelayCommand openCounterPartyMenu;
        public RelayCommand OpenCounterPartyMenu
        {
            get
            {
                return openCounterPartyMenu ?? new RelayCommand(obj =>
                {
                    CurrentPage.Сounterparty = obj as Conterpaty;
                    if (Conterpaty.LegalEntity.Visibility == Visibility.Hidden)
                    {
                        Conterpaty.LegalEntity.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Conterpaty.LegalEntity.Visibility = Visibility.Hidden;
                    }
                    Conterpaty.LegalEntityFrame.Navigate(new System.Uri("Views/CounterPartyMenu.xaml", UriKind.RelativeOrAbsolute));
                }
                );
            }
        }

        private RelayCommand openCounterPartySecondMenu;
        public RelayCommand OpenCounterPartySecondMenu
        {
            get
            {
                return openCounterPartySecondMenu ?? new RelayCommand(obj =>
                {
                    CurrentPage.Сounterparty = obj as Conterpaty;
                    if (Conterpaty.CounterParty.Visibility == Visibility.Hidden)
                    {
                        Conterpaty.CounterParty.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Conterpaty.CounterParty.Visibility = Visibility.Hidden;
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                
                    Conterpaty.CounterPartyFrame.Navigate(new System.Uri("Views/CounterPartySecondMenu.xaml", UriKind.RelativeOrAbsolute));

                }

                );
            }
        }

        private void PageActionSecondMenu()
        {
            if (Conterpaty.CounterParty.Visibility == Visibility.Hidden)
            {
                Conterpaty.CounterParty.Visibility = Visibility.Visible;
            }
            else
            {
                Conterpaty.CounterParty.Visibility = Visibility.Hidden;
            }
        }

        private RelayCommand closeCOunterPartySecondMenu;
        public RelayCommand btncloseCOunterPartySecondMenu
        {
            get
            {
                return closeCOunterPartySecondMenu ?? new RelayCommand(obj =>
                {
                    PageActionSecondMenu();
                }
                );
            }
        }

        #endregion

        #region Свойства страницы
        //свойства для Юрлица 
        private static string legalEntity_Name { get; set; }

        public string LegalEntity_Name
        {
            get { return legalEntity_Name; }
            set
            {
                legalEntity_Name = value;
                AllLegalEntityById = DataWorker.GetAllLegalEntityById();
                NotifyPropertyChanged("LegalEntity_Name");

            }
        }

        private static string legalEntity_FullName { get; set; }

        public string LegalEntity_FullName
        {
            get { return legalEntity_FullName; }
            set
            {
                legalEntity_FullName = value;
                AllLegalEntityById = DataWorker.GetAllLegalEntityById();
                NotifyPropertyChanged("LegalEntity_FullName");
            }
        }

        private static string legalEntity_Comment { get; set; }

        public string LegalEntity_Comment
        {
            get { return legalEntity_Comment; }
            set
            {
                legalEntity_Comment = value;
                AllLegalEntityById = DataWorker.GetAllLegalEntityById();
                NotifyPropertyChanged("LegalEntity_Comment");

            }
        }


        // Свойство для Контрагента
        public static string counterparty_Name { get; set; }

        public string Сounterparty_Name
        {
            get { return counterparty_Name; }
            set
            {
                counterparty_Name = value;
                NotifyPropertyChanged("Сounterparty_Name");
            }
        }

        public static string counterparty_Comment { get; set; }
        public string Сounterparty_Comment
        {
            get { return counterparty_Comment; }
            set
            {
                counterparty_Comment = value;
                NotifyPropertyChanged("Сounterparty_Comment");
            }
        }

        #endregion

        private void SetWindow(Page wnd)
        {
            AllLegalEntityById = DataWorker.GetAllLegalEntityById();
            CurrentPage.Сounterparty.ViewAllLegalEntity.ItemsSource = AllLegalEntityById;
            CurrentPage.Сounterparty.ViewAllLegalEntity.Items.Refresh();
        }
        private void SetWindow2(Page wnd)
        {
        
            AllcounterPartiesById = DataWorker.GetAllСounterpartyById();
          
            Conterpaty.CounterpartyList.ItemsSource = AllcounterPartiesById;
            Conterpaty.CounterpartyList.Items.Refresh();
        }

        #region COMMANDS TO ADD
        // Добавление Юр Лица  
        private RelayCommand addNewLegalEntity;
        public RelayCommand AddNewLegalEntity
        {
            get
            {
                return addNewLegalEntity ?? new RelayCommand(obj =>
                {
                   
                    string resultStr = "";
                 
                        resultStr = DataWorker.CreatLegalEntity(LegalEntity_Name, LegalEntity_FullName, LegalEntity_Comment, SaveUser.CurrentUser);
                        SetWindow(CurrentPage.Сounterparty);
                        SetNullValuesToProperties();
                        
                        PageAction();
                       
                    
                        PaymentPage.LegalEntityBox.ItemsSource = DataWorker.GetAllLegalEntityById();
                        Conterpaty.CountofL.Text = AllLegalEntityById.Count.ToString();

                    
                }
                );
            }
        }

        // Добавление контрагента
        private RelayCommand addNewСounterparty;
        public RelayCommand AddNewСounterparty
        {
            get
            {
                return addNewСounterparty ?? new RelayCommand(obj =>
                {
                    //Window wnd = obj as Window;
                   
                
                     DataWorker.CreatConterparty(Сounterparty_Name, Сounterparty_Comment, SaveUser.CurrentUser);
                     PageActionSecondMenu();

                    AllcounterPartiesById = DataWorker.GetAllСounterpartyById();

                    Conterpaty.CounterpartyList.ItemsSource = AllcounterPartiesById;
                    Conterpaty.CounterpartyList.Items.Refresh();

                    SetNullValuesToProperties();
              
                        HomePage.Counterparty.ItemsSource = AllcounterPartiesById;
                        HomePage.CounterpartyP.ItemsSource = AllcounterPartiesById;
                        Conterpaty.CountofC.Text = AllcounterPartiesById.Count.ToString();

                    
                }
                );
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

        //  Все контрагенты  по айди пользователя
        private ObservableCollection<Сounterparty> _allcounterPartiesById = DataWorker.GetAllСounterpartyById();
        public ObservableCollection<Сounterparty> AllcounterPartiesById
        {
            get { return _allcounterPartiesById; }
            set
            {
                _allcounterPartiesById = value;
                RaisePropertyChanged("AllcounterPartiesById");

            }
        }
        #endregion

        #region UPDATE VIEWS
        private void SetNullValuesToProperties()
        {
            //для юрлица
            LegalEntity_Name = null;
            LegalEntity_FullName = null;
            LegalEntity_Comment = null;

            //для контрагента
            Сounterparty_Name = null;
            Сounterparty_Comment = null;
        }

        // Свойство поиска Юл лица
        private static string legalEntityName { get; set; }
        public string LegalEntityName
        {
            get { return legalEntityName; }
            set
            {
                legalEntityName = value;
                AllLegalEntityById = DataWorker.GetFindLegalEntityById(value);
                Conterpaty.LegalEntityList.ItemsSource = AllLegalEntityById;
                NotifyPropertyChanged("LegalEntityName");

            }
        }

        // Свойство поиска контрагента
        private static string сonterPartyName { get; set; }
        public string ConterPartyName
        {
            get { return сonterPartyName; }
            set
            {
                сonterPartyName = value;
                //AllcounterPartiesById  = DataWorker.GetFindCounterPartyById(value);
                //CurrentPage.Сounterparty.ViewAllCounterParty.ItemsSource = AllcounterPartiesById;
                NotifyPropertyChanged("ConterPartyName");

            }
        }



        #endregion

        //////////////
        /// Изменение
        //////////////
        #region Изменение 


        // Изменение контрагента
        private RelayCommand сhangeLegalEntity;
        public RelayCommand ChangeLegalEntity
        {
            get
            {
                return сhangeLegalEntity ?? new RelayCommand(obj =>
                {
                 
                    Conterpaty.CounterParty.Visibility = Visibility.Visible;
                    Conterpaty.LegalEntityChange.Visibility = Visibility.Visible;
                    LegalEntity_Namec = LegalEntityNa.LegalEntity_Name;
                    LegalEntity_FullNamec = LegalEntityNa.LegalEntity_FullName;
                    LegalEntity_Commentc = LegalEntityNa.LegalEntity_Comment;


                }
                )
                {
                };
            }
        }

        public ObservableCollection<Account> AllaccountsById { get; set; }

        private RelayCommand changeNewLegalEntity;
        public RelayCommand ChangeNewLegalEntity
        {
            get
            {
                return changeNewLegalEntity ?? new RelayCommand(obj =>
                {
                   
                    string resultStr = "";
                  
                    resultStr = DataWorker.EditLegalEntity(LegalEntityNa,LegalEntity_Namec, LegalEntity_FullNamec, LegalEntity_Commentc);
                    AllLegalEntityById = DataWorker.GetAllLegalEntityById();
                    Conterpaty.LegalEntityList.ItemsSource = null;
                    Conterpaty.LegalEntityList.Items.Clear();
                    Conterpaty.LegalEntityList.ItemsSource = AllLegalEntityById;
                    Conterpaty.LegalEntityList.Items.Refresh();
                 
                    Conterpaty.CounterParty.Visibility = Visibility.Hidden;
                    Conterpaty.LegalEntityChange.Visibility = Visibility.Hidden;
                    AllaccountsById = DataWorker.GetAllAccountById();
                    PaymentPage.AccountList.ItemsSource = AllaccountsById;

                    SetNullValuesToProperties();

                       
                    PaymentPage.LegalEntityBox.ItemsSource = AllLegalEntityById;
                     

                    
                }
                );
            }
        }


        private static string legalEntity_Namec { get; set; }

        public string LegalEntity_Namec
        {
            get { return legalEntity_Namec; }
            set
            {
                legalEntity_Namec = value;
                AllLegalEntityById = DataWorker.GetAllLegalEntityById();
                NotifyPropertyChanged("LegalEntity_Namec");

            }
        }

        private static string legalEntity_FullNamec { get; set; }

        public string LegalEntity_FullNamec
        {
            get { return legalEntity_FullNamec; }
            set
            {
                legalEntity_FullNamec = value;
                AllLegalEntityById = DataWorker.GetAllLegalEntityById();
                NotifyPropertyChanged("LegalEntity_FullNamec");
            }
        }

        private static string legalEntity_Commentc { get; set; }

        public string LegalEntity_Commentc
        {
            get { return legalEntity_Commentc; }
            set
            {
                legalEntity_Commentc = value;
                AllLegalEntityById = DataWorker.GetAllLegalEntityById();
                NotifyPropertyChanged("LegalEntity_Commentc");

            }
        }

        private RelayCommand btnclosechange;
        public RelayCommand btnclosechangel
        {
            get
            {
                return btnclosechange ?? new RelayCommand(obj =>
                {
                    Conterpaty.CounterParty.Visibility = Visibility.Hidden;
                    Conterpaty.LegalEntityChange.Visibility = Visibility.Hidden;

                }
                );
            }
        }




        #region  Редактирование контрагента

       

        private static string сounterparty_NameСh { get; set; }

        public string Сounterparty_NameСh
        {
            get { return сounterparty_NameСh; }
            set
            {
                сounterparty_NameСh = value;
                NotifyPropertyChanged("Сounterparty_NameСh");

            }
        }

        private static string сounterparty_CommentCh { get; set; }

        public string Сounterparty_CommentCh
        {
            get { return сounterparty_CommentCh; }
            set
            {
                сounterparty_CommentCh = value;
                NotifyPropertyChanged("Сounterparty_CommentCh");

            }
        }


        // Изменение контрагента
        private RelayCommand changeCounterparty;
        public RelayCommand ChangeCounterparty
        {
            get
            {
                return changeCounterparty  ?? new RelayCommand(obj =>
                {

                    Conterpaty.CounterParty.Visibility = Visibility.Visible;
                    Conterpaty.CounterPartyChange.Visibility = Visibility.Visible;
                    Сounterparty_NameСh = SelectCounterparty.Сounterparty_Name;
                    Сounterparty_CommentCh = SelectCounterparty.Сounterparty_Comment;

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

                    string resultStr = "";
                    MessageBox.Show("Изменен");
                    resultStr = DataWorker.EditConterparty(SelectCounterparty, Сounterparty_NameСh, Сounterparty_CommentCh);
                    AllcounterPartiesById = DataWorker.GetAllСounterpartyById();
                    Conterpaty.CounterpartyList.ItemsSource = null;
                    Conterpaty.CounterpartyList.Items.Clear();
                    Conterpaty.CounterpartyList.ItemsSource = AllcounterPartiesById;
                    Conterpaty.CounterpartyList.Items.Refresh();

                    Conterpaty.CounterParty.Visibility = Visibility.Hidden;
                    Conterpaty.CounterPartyChange.Visibility = Visibility.Hidden;

                    SetNullValuesToProperties();


                    HomePage.Counterparty.ItemsSource = AllcounterPartiesById;
                    HomePage.CounterpartyP.ItemsSource = AllcounterPartiesById;
                    HomePage.Entrance.ItemsSource = DataWorker.GetAllEntranceById();
                    HomePage.Payment.ItemsSource = DataWorker.GetAllPaymentById();
                }
                );
            }
        }

        private RelayCommand closeEditCounterParty;
        public RelayCommand CloseEditCounterParty
        {
            get
            {
                return closeEditCounterParty ?? new RelayCommand(obj =>
                {

                    Conterpaty.CounterParty.Visibility = Visibility.Hidden;
                    Conterpaty.CounterPartyChange.Visibility = Visibility.Hidden;

                });
            }
        }

        #endregion

        #endregion
    }

}