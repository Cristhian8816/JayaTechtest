using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace jayatechTest.Models
{
    public partial class message
    {
        public int id_message { get; set; }

        public int id_User { get; set; }

        public int id_Room { get; set; }

        public string messageText { get; set; }

        public DateTime msnDate { get; set; }

    }
   
}
