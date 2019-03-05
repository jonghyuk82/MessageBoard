using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Board.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}