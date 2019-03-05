using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Board.Models
{
    public class SubPost
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime RegisterDate { get; set; }
        public int PostID { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<SubPostFile> SubPostFile { get; set; }

    }
}