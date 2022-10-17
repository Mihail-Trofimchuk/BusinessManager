
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Model
{
    public class User : Repository.Entity.Entity
    {
        [Key]
        public int User_Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        public List<LegalEntity> LegalEntities { get; set; } = new List<LegalEntity>();
        public List<Account> Accounts { get; set; } = new List<Account>();
        public List<Projects> Projects { get; set; } = new List<Projects>();
      
    }
}
