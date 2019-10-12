using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace jayatechTest.Models
{
    //This is the view model that contains info of Users, message and rooms tables 
    public partial class User_Message
    {
        public Users user { get; set; }
        public ChatRooms ChatRoom { get; set; }
        public message message { get; set; }

        public string NickName { get; set; }

        public string messageText { get; set; }
        public DateTime  DateMessage { get; set; }

        public int userID { get; set; }

        public int chatroom { get; set; }

        public string roomName { get; set; }
        public string text { get; set; }

        public string sentMessage { get; set; }

        public List<string> Rooms { get; set; }

        public List<string> nicknames { get; set; }
        public List<int> userIDs { get; set; }

    }
   
}
