using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace jayatechTest.Models
{
    public partial class User_Message
    {
        public Users user { get; set; }
        public message message { get; set; }

        public int userID { get; set; }

        public int chatroom { get; set; }

        public string text { get; set; }

        public string sentMessage { get; set; }        

    }
   
}
