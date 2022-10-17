using BusinessManager.Model.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessManager.Model
{
    public class Messages
    {


        [Key]
        public int MessagesId { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

        public string Message_Type { get; set; }
        public string Message_Title { get; set; }
        public int Message_Cost { get; set; }

        [NotMapped]
        public User UserById
        {
            get
            {
                return DataWorker.GetAllUsersByID(UserId);
            }
        }

    }   
}
