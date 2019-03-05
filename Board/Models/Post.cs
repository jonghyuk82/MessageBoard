using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Board.Models
{
    public class Post
    {
        public int ID { get; set; }
        public int UserID { get; set; }

        [Required(ErrorMessage ="Please enter title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter message.")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Please select status.")]
        [Display(Name ="Status")]
        public int StatusID { get; set; }

        [Required(ErrorMessage = "Please select importance.")]
        [Display(Name = "Importance")]
        public int ImportanceID { get; set; }

        public int PriorityID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegisterDate { get; set; }

        public virtual User User { get; set; }
        public virtual Status Status { get; set; }
        public virtual Importance Importance { get; set; }
        public virtual Priority Priority { get; set; }

        public virtual ICollection<SubPost> SubPost { get; set; }
        public virtual ICollection<PostFile> PostFile { get; set; }
        public virtual ICollection<SubPostFile> SubPostFile { get; set; }
    }
}