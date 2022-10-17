
using System.ComponentModel.DataAnnotations;


namespace BusinessManager.Model
{
    public class LegalEntity
    {
        [Key]
       
        public int LegalEntity_Id{ get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

        public string LegalEntity_Name { get; set; }
        public string LegalEntity_FullName { get; set; }
        public string LegalEntity_Comment { get; set; }

    }
}