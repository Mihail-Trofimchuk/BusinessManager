using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Model
{
    public class Moving
    {
        [Key]
  
        public int Moving_Id { get; set; }

        public DateTime Moving_Day { get; set; }

       

        public Account MovingInAccount_Id { get; set; }

        public Account MovingOutAccount_Id { get; set; }

        public decimal Moving_Sum { get; set; }

        public string MovingPurpose { get; set; }


    }
}