using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Board.Models
{
    public class SubPostFile
    {
        public int ID { get; set; }

        public string FileName { get; set; }
        public string Extension { get; set; }
        public int SubPostID { get; set; }
        public virtual SubPost SubPost { get; set; }
    }
}