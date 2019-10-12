using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace jayatechTest.Models
{
    //This is de model view that contains info of ChatRooms and message Db tables
    public partial class Rooms_Message
    {
        public ChatRooms ChatRoom { get; set; }
        public message Message { get; set; }

        public string ChatRoomName { get; set; }

        public string messageText { get; set; }

        public string UserName { get; set; }

        public DateTime DateMessage { get; set; }
    }
   
}
