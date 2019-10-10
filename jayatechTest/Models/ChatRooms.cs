using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace jayatechTest.Models
{
    public partial class ChatRooms
    {
        public int id_Room { get; set; }

        public int id_User { get; set; }

        public string roomName { get; set; }

        public int deleteRoom { get; set; }

        public DateTime created_at { get; set; }
      
    }
   
}
