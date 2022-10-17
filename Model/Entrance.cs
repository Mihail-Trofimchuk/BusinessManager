using BusinessManager.Model.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessManager.Model
{
   
    public class Entrance
    {
        [Key]
      
        public int Entrance_Id { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

        public DateTime? Entrance_Day { get; set; }

        public int Account_Id { get; set; }
        public Account EntranceAccount_Id { get; set; }

        public decimal? Entrance_Sum { get; set; }

        public string Entrance_article { get; set; }

        public int Projects_Id { get; set; }
        public Projects EntranceProjects_Id { get; set; }

        public int Counterparty_Id { get; set; }
        public Сounterparty EntranceСounterparty_Id { get; set; }

        public string EntrancePurpose { get; set; }


        [NotMapped]
        public Account AccountEntrance
        {
            get
            {
                return DataWorker.GetAccountById(Account_Id);
            }
        }
        [NotMapped]
        public Projects ProjectEntrance
        {
            get
            {
                return DataWorker.GetProjectsById(Projects_Id);
            }
        }

        [NotMapped]
        public Сounterparty CounterpartyEntrance
        {
            get
            {
                return DataWorker.GetCounterpartyById(Counterparty_Id);
            }
        }


    }
}