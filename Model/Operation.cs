using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Model
{
    public class Operation
    {
        [Key]
       
        public int Operation_Id { get; set; }
        public Entrance Entrance {get; set; }

        public Payment Payment { get; set; }

        public Moving Moving { get; set; }
    
    }
}