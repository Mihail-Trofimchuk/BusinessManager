using BusinessManager.Model.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessManager.Model
{
    public class Account
    {

        [Key]
        public int Account_Id { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

        public string Account_Name { get; set; }


        public int LegalEntity_IdE { get; set; }
        public LegalEntity Legal { get; set; } 

       
        public string Account_Type { get; set; }

       
        public decimal? CurrentBalance { get; set; }

        public string Account_Comment { get; set; }

        public DateTime? AccountDateImput { get; set; }

        [NotMapped]
        public LegalEntity AccountLegalEntity
        {
            get
            {
                return DataWorker.GetLegalEntityById(LegalEntity_IdE);
            }
        }

        //public string Сurrency { get; set; }


    }
}