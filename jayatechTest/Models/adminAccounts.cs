using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace jayatechTest.Models
{
    public partial class adminAccounts
    {
        public int id_admin { get; set; }

        public string userName { get; set; }

        public string password { get; set; }      
      
    }
   
}
