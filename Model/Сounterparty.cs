using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Model
{
    public class Сounterparty
    {
        [Key]
        
        public int Сounterparty_Id { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

        public string Сounterparty_Name { get; set; }

        public string Сounterparty_Comment { get; set; }
    }
}
