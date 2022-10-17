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
    public class ProjectViewModel : INotifyPropertyChanged
    {
        public ProjectViewModel()
        { }
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
        private RelayCommand openProjectMenu;
        public RelayCommand OpenProjectMenu
        {
            get
            {
                return openProjectMenu ?? new RelayCommand(obj =>
                {
                    CurrentPage.ProjectsPage = obj as SoundsPage;
                    if (SoundsPage.ProgectsGrid.Visibility == Visibility.Hidden)
                    {
                        SoundsPage.ProgectsGrid.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        SoundsPage.ProgectsGrid.Visibility = Visibility.Hidden;
                    }

                    SoundsPage.ProgectsFrame.Navigate(new System.Uri("Views/ProjectMenu.xaml", UriKind.RelativeOrAbsolute));
                }
                );
            }
        }

        private RelayCommand closeProjectMenu;
        public RelayCommand btncloseProjectMenu
        {
            get
            {
                return closeProjectMenu ?? new RelayCommand(obj =>
                {
                    PageAction();
                }
                );
            }
        }

        private void PageAction()
        {
            if (SoundsPage.ProgectsGrid.Visibility == Visibility.Hidden)
            {
                SoundsPage.ProgectsGrid.Visibility = Visibility.Visible;
            }
            else
            {
                SoundsPage.ProgectsGrid.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        #region  Свойства 

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


        private string projectName { get; set; }
        public string ProjectName
        {
            get { return projectName; }
            set
            {
                projectName = value;
                NotifyPropertyChanged("ProjectName");
            }
        }

        private string projectComment { get; set; }
        public string ProjectComment
        {
            get { return projectComment; }
            set
            {
                projectComment = value;
                NotifyPropertyChanged("ProjectComment");
            }
        }

        private Nullable<DateTime> projectStartTime = null;
        public Nullable<DateTime> ProjectStartTime
        {
            get
            {
                if (projectStartTime == null)
                {
                    projectStartTime = DateTime.Today;
                }
                return projectStartTime;
            }
            set
            {
                projectStartTime = value;
                RaisePropertyChanged("ProjectStartTime");
            }
        }

       

        #endregion

        #region Действия с проектами

        // Добавление проекта
        private RelayCommand addNewProject;
        public RelayCommand AddNewProject
        {
            get
            {
                return addNewProject ?? new RelayCommand(obj =>
                {
                    //Window wnd = obj as Window;
                    string resultStr = "";
                    if (ProjectName == null || ProjectName.Replace(" ", "").Length == 0)
                    {
                        MessageBox.Show("НеДобавлен");
                        //SetRedBlockControll(wnd, "NameBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateProject(ProjectName, ProjectComment, ProjectStartTime, SaveUser.CurrentUser);
                        PageActionMenu();
                        SetWindow(CurrentPage.ProjectsPage);
                        SetNullValuesToProperties();
                        HomePage.Projects.ItemsSource = AllProjectsById;
                        HomePage.ProjectsP.ItemsSource = AllProjectsById;
                        SoundsPage.CountofProjects.Text = AllProjectsById.Count.ToString();
                        NotesPage.ListofProjects.ItemsSource = AllProjectsById;

                    }
                }
                );
            }
        }

        private void SetWindow(Page wnd)
        {
            AllProjectsById = DataWorker.GetAllProjectsById();
           
            CurrentPage.ProjectsPage.AllProjects.ItemsSource = AllProjectsById;
            CurrentPage.ProjectsPage.AllProjects.Items.Refresh();
        }
        private void PageActionMenu()
        {
            if (SoundsPage.ProgectsGrid.Visibility == Visibility.Hidden)
            {
                SoundsPage.ProgectsGrid.Visibility = Visibility.Visible;
            }
            else
            {
                SoundsPage.ProgectsGrid.Visibility = Visibility.Hidden;
            }
        }
        private void SetNullValuesToProperties()
        {
            ProjectName = null;
            ProjectComment = null;
            ProjectNameEd = null;
            ProjectCommentEd = null;
            ProjectStartTimeEd = null;



        }
        #endregion


        #region Поиск 

        // Свойство поиска проекта
        private static string projectNameSearch { get; set; }
        public string ProjectNameSearch
        {
            get { return projectNameSearch; }
            set
            {
                projectNameSearch = value;
                AllProjectsById = DataWorker.GetFindProjectById(value);
                SoundsPage.ViewAllProject.ItemsSource = AllProjectsById;
                NotifyPropertyChanged("ProjectNameSearch");

            }
        }

        // Команда получение окна при поиске
        private RelayCommand searchpr;
        public RelayCommand SearchProjects
        {
            get
            {
                return searchpr ?? new RelayCommand(obj =>
                {
                    CurrentPage.ProjectsPage = obj as SoundsPage;
                }
                );
            }
        }

        // Свойство поиска проекта
        private static DateTime projectDateSearch = DateTime.Today;
    public DateTime ProjectDateSearch
        {
            get 
            {
                return projectDateSearch;
            }
            set
            {
                if (projectDateSearch == null)
                {
                    projectDateSearch = DateTime.Today;
                }
                projectDateSearch = value;
                AllProjectsById = DataWorker.GetFindProjectByDateId(value);
                SoundsPage.ViewAllProject.ItemsSource = AllProjectsById;
                NotifyPropertyChanged("ProjectDateSearch");

            }
        }

        // Удаление проета
       
        public ObservableCollection<Entrance> Entrance { get; set; }
        public ObservableCollection<Payment> Payment { get; set; }
        private RelayCommand deleteproject;
        public RelayCommand DeleteProject
        {
            get
            {
                return deleteproject ?? new RelayCommand(obj => {

                    DataWorker.DeleteProject(SelectProject);
                    AllProjectsById = DataWorker.GetAllProjectsById();
                    SoundsPage.ViewAllProject.ItemsSource = null;
                    SoundsPage.ViewAllProject.Items.Clear();
                    SoundsPage.ViewAllProject.ItemsSource = AllProjectsById;
                    SoundsPage.ViewAllProject.Items.Refresh();
                    Entrance = DataWorker.GetAllEntranceById();
                    Payment = DataWorker.GetAllPaymentById();
                    HomePage.Payment.ItemsSource = null;
                    HomePage.Payment.Items.Clear();
                    HomePage.Payment.ItemsSource = Payment;
                    HomePage.Payment.Items.Refresh();
                    HomePage.PaymentCount.Text = Payment.Count.ToString();
                    HomePage.Entrance.ItemsSource = null;
                    HomePage.Entrance.Items.Clear();
                    HomePage.Entrance.ItemsSource = Entrance;
                    HomePage.Entrance.Items.Refresh();
                    HomePage.EntranceCount.Text = Entrance.Count.ToString();
                    SoundsPage.CountofProjects.Text = AllProjectsById.Count.ToString();
                    HomePage.Projects.ItemsSource = AllProjectsById;
                    HomePage.ProjectsP.ItemsSource = AllProjectsById;

                }
                );
            }
        }

        // Выбранный проект
        private Projects selectproject;
        public Projects SelectProject
        {
            get { return selectproject; }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectproject = value;
                NotifyPropertyChanged("SelectProject");
            }
        }
        #endregion

        #region Редактирование

        private RelayCommand сhangeProject;
        public RelayCommand ChangeProject
        {
            get
            {
                return сhangeProject ?? new RelayCommand(obj =>
                {
                    SoundsPage.ProjectBorder.Visibility = Visibility.Visible;
                    SoundsPage.ProgectsGrid.Visibility = Visibility.Visible;
                    ProjectNameEd = SelectProject.Projects_Name;
                    ProjectCommentEd = SelectProject.Projects_Comment;
                    ProjectStartTimeEd = selectproject.Projects_StartTime;
                }
                );
            }
        }

        public ObservableCollection<Account> AllaccountsById { get; set; }

        private RelayCommand changeNewProject;
        public RelayCommand ChangeNewProject
        {
            get
            {
                return changeNewProject ?? new RelayCommand(obj =>
                {

                    string resultStr = "";

                    resultStr = DataWorker.EditProject(SelectProject, ProjectNameEd, ProjectCommentEd, ProjectStartTimeEd);
                    AllProjectsById = DataWorker.GetAllProjectsById();
                    SoundsPage.ViewAllProject.ItemsSource = null;
                    SoundsPage.ViewAllProject.Items.Clear();
                    SoundsPage.ViewAllProject.ItemsSource = AllProjectsById;
                    SoundsPage.ViewAllProject.Items.Refresh();

                    SoundsPage.ProjectBorder.Visibility = Visibility.Hidden;
                    SoundsPage.ProgectsGrid.Visibility = Visibility.Hidden;

                    HomePage.Projects.ItemsSource = AllProjectsById;
                    HomePage.ProjectsP.ItemsSource = AllProjectsById;
                    HomePage.Entrance.ItemsSource = DataWorker.GetAllEntranceById();
                    HomePage.Payment.ItemsSource = DataWorker.GetAllPaymentById();
                    NotesPage.ListofProjects.ItemsSource = AllProjectsById;

                    SetNullValuesToProperties();

                }
                );
            }
        }

        private static string projectNameEd { get; set; }

        public string ProjectNameEd
        {
            get { return projectNameEd; }
            set
            {
                projectNameEd = value;
                NotifyPropertyChanged("ProjectNameEd");

            }
        }



        private static string projectCommentEd { get; set; }

        public string ProjectCommentEd
        {
            get { return projectCommentEd; }
            set
            {
                projectCommentEd = value;
                
                NotifyPropertyChanged("ProjectCommentEd");
            }
        }

        private Nullable<DateTime> projectStartTimeEd = null;
        public Nullable<DateTime> ProjectStartTimeEd
        {
            get
            {
                if (projectStartTimeEd == null)
                {
                    projectStartTimeEd = DateTime.Today;
                }
                return projectStartTimeEd;
            }
            set
            {
                projectStartTimeEd = value;
                RaisePropertyChanged("ProjectStartTimeEd");
            }
        }



 

        private RelayCommand btnclosechange;
        public RelayCommand btnclosechangel
        {
            get
            {
                return btnclosechange ?? new RelayCommand(obj =>
                {
                    SoundsPage.ProjectBorder.Visibility = Visibility.Hidden;
                    SoundsPage.ProgectsGrid.Visibility = Visibility.Hidden;

                }
                );
            }
        }



        #endregion

    }

}
