using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jayatechTest.Models;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;

namespace jayatechTest.Controllers
{
    public class AdminController : Controller //Controller actions for Admin application Module
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminLogin(adminAccounts user)
        {
            return View();                     
        }  

        public IActionResult ProcessLogin(adminAccounts user)
        {            
            using (UsersContext db = new UsersContext())
            {
                //We recieve the login information from form view and we have to looking for in database to certificate the credentials
                adminAccounts usr = db.adminAccounts.Where(u => u.userName == user.userName && u.password == user.password).FirstOrDefault(); 
                if (usr != null)//if our login is suceesfull
                {                    
                    User_Message user_messages = new User_Message();

                    //Here we bring the all nicknames register in database and their respective ids
                    List<int> usersIds = new List<int>();
                    List<string> NicekNames = new List<string>();

                    IEnumerable<Users> nicknames = null;

                    nicknames = (from users in db.Users
                             select new Users
                             {
                                id_User = users.id_User,
                                NickName= users.NickName,
                             }
                            );

                    //Completed lists with the information brought
                    foreach (var name in nicknames)
                    {
                        NicekNames.Add(name.NickName);
                        usersIds.Add(name.id_User);
                    }

                    //pass the information from lists to lists's modelview to expose in a view
                    user_messages.nicknames = NicekNames;
                    user_messages.userIDs = usersIds;

                    //here we repetat the same process to bring the informtion from Chatrooms in database
                    List<string> roomNames = new List<string>();

                    IEnumerable<ChatRooms> roomchatNames = null;

                    roomchatNames = (from chatroom in db.ChatRooms
                                    select new ChatRooms
                                    { 
                                        id_Room = chatroom.id_Room,                       
                                        roomName = chatroom.roomName,                                       
                                    }
                            );


                    foreach (var roomnames in roomchatNames)
                    {
                        roomNames.Add(roomnames.roomName);
                    }

                    user_messages.Rooms = roomNames;

                    return View("AdminControl", user_messages);
                }
                else
                {
                    ModelState.AddModelError("userName", "User name or password incorrect");
                    return View("AdminLogin");
                }
            }
        }
        public IActionResult userHistory(User_Message model)
        {
            //After select the user that we want to know the history messages, we need to bring his written messages
            UsersContext db = new UsersContext();
            
            var usr = db.Users.Where(u => u.NickName == model.NickName).FirstOrDefault();
            if (usr != null)
            {
                IEnumerable<User_Message> historyUser = null;

                historyUser = (from user in db.Users
                            join msg in db.message
                            on user.id_User equals msg.id_User

                            where user.id_User == usr.id_User

                            select new User_Message
                            {
                                NickName = user.NickName,
                                messageText = msg.messageText,
                                DateMessage = msg.msnDate,
                            }
                        );
                return View(historyUser);                    
            }
            else
            {
                return View("AdminLogin");
            }
            
        }
        public IActionResult historyRoom(User_Message model)
        {
            //After select the room that we want to know the history messages, we need to bring the written messages of each users in this chat room
            UsersContext db = new UsersContext();

            var Room = db.ChatRooms.Where(ch => ch.roomName == model.roomName).FirstOrDefault();
            if (Room != null)
            {
                IEnumerable<Rooms_Message> historyRoom = null;

                historyRoom = (from room in db.ChatRooms
                               join msg in db.message
                               on room.id_Room equals msg.id_Room
                               join usr in db.Users
                              on msg.id_User equals usr.id_User

                               where room.id_Room == Room.id_Room

                               select new Rooms_Message
                                {
                                    ChatRoomName = room.roomName,
                                    UserName = usr.NickName,
                                    messageText = msg.messageText,
                                    DateMessage = msg.msnDate,
                                }                             
                 
                        );
                return View(historyRoom);
            }
            else
            {
                return View("AdminLogin");
            }
            
        }

    }
}
