using BusinessManager.Model.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessManager.Model
{
    public class Payment
    {
        [Key]
       
        public int Payment_Id { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

        public DateTime? Payment_Day { get; set; }

        public int Account_Id { get; set; }
        public Account PaymentAccount_Id { get; set; }

        public decimal? Payment_Sum { get; set; }
       
        public string Payment_article { get; set; }

        public int Projects_Id { get; set; }
        public Projects PaymentProjects_Id { get; set; }

        public int Counterparty_Id { get; set; }
        public Сounterparty PaymentСounterparty_Id { get; set; }

        public string PaymentPurpose { get; set; }

        [NotMapped]
        public Account AccountPayment
        {
            get
            {
                return DataWorker.GetAccountById(Account_Id);
            }
        }
        [NotMapped]
        public Projects ProjectPayment
        {
            get
            {
                return DataWorker.GetProjectsById(Projects_Id);
            }
        }

        [NotMapped]
        public Сounterparty CounterpartyPayment
        {
            get
            {
                return DataWorker.GetCounterpartyById(Counterparty_Id);
            }
        }

    }
}