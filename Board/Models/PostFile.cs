using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Board.Models
{
    public class PostFile
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int PostID { get; set; }

        public virtual Post Post { get; set; }
    }
}