
using System.ComponentModel.DataAnnotations;


namespace BusinessManager.Model
{
    public class Article
    {

        [Key]
        public int ArticleId { get; set; }

        public string ArticleType { get; set; }

        public string ArticleTitle { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

    }
}
