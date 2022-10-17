using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Model
{
    public class Cost
    {
        [Key]
        public int CostId { get; set; }
        public string CostName { get; set; }
        
        public double CostValue { get; set; }

        public int UserId { get; set; }

        public User user { get; set; }
    }
}
