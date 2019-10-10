using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace jayatechTest.Models
{
    public partial class Users
    {
        public int id_User { get; set; }

        public string NickName { get; set; }

        public int Active { get; set; }
      
        public DateTime Date { get; set; }
      
    }
   
}
