
using System;
using System.ComponentModel.DataAnnotations;


namespace BusinessManager.Model
{
    public class Projects
    {
        
        [Key]
        public int ProjectsId { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

        public string Projects_Name { get; set; }
        public string Projects_Comment { get; set; }

      
        public Nullable<DateTime> Projects_StartTime { get; set; }

    }
}