using BusinessManager.Model;
using System;
using System.ComponentModel;

namespace BusinessManager.ViewModel
{
       public  class OperationCommandsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        //свойства для Поступления
        public static DateTime Entrance_Day { get; set; }
      
        public static Account EntranceAccount_Id { get; set; }
        public static decimal Entrance_Sum { get; set; }
        public static string Entrance_article { get; set; }
        public static Projects EntranceProjects_Id { get; set; }
        public static Сounterparty EntranceСounterparty_Id { get; set; }
        public static string EntrancePurpose { get; set; }


        /// 


        #region COMMANDS TO ADD
        //private RelayCommand addNewEntrance;
        //public RelayCommand AddNewEntrance
        //{
        //    get
        //    {
        //        return addNewEntrance ?? new RelayCommand(obj =>
        //        {
        //           // Window wnd = obj as Window;
        //            string resultStr = "";
        //            if (Entrance_article == null || Entrance_article.Replace(" ", "").Length == 0)
        //            {
        //                MessageBox.Show("Rkjey");
        //                //SetRedBlockControll(wnd, "NameBlock");
        //            }
        //            else
        //            {
        //                resultStr = DataWorker.CreatEntrance(Entrance_Day, EntranceAccount_Id, Entrance_Sum, Entrance_article, EntranceProjects_Id, EntranceСounterparty_Id, EntrancePurpose);
        //               // UpdateAllDataView();

        //                //ShowMessageToUser(resultStr);
        //                //SetNullValuesToProperties();
        //                //wnd.Close();
        //            }
        //        }
        //        );
        //    }
        //}
        #endregion



    }
}
