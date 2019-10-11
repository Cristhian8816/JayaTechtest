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
    public class HomeController : Controller
    {
     
        public IActionResult Index()
        {
            UsersContext db = new UsersContext();

            IEnumerable<ChatRooms> model = null;

            model = (from chatroom in db.ChatRooms                     
                     select new ChatRooms
                     {
                         id_Room = chatroom.id_Room,                       
                         roomName = chatroom.roomName,
                         deleteRoom = chatroom.deleteRoom,
                         created_at = chatroom.created_at,
                     }
                    );
            return View(model);            
        }

        public IActionResult procesNickName(Users model, int room_ID)
        {
            using (UsersContext db = new UsersContext())
            {
                var nickname = db.Users.Where(n => n.NickName == model.NickName).FirstOrDefault();
                
                if(nickname != null)
                {
                    var userActiva = db.Users.Where(a => a.Active == nickname.Active).FirstOrDefault();                                 

                    if(userActiva != null)
                    {                                          
                        ModelState.AddModelError("NickName", "This user is logged in this chat room");
                        return View("nickName");
                    }
                    else
                    {
                        Users user = new Users();
                        var count_users = db.Users.Count();
                        count_users++;


                        user.id_User = count_users;
                        user.NickName = model.NickName;
                        user.Active = 1;
                        user.Date = DateTime.Now;

                        db.Users.Add(user);
                        db.SaveChanges();

                        User_Message user_message = new User_Message();
                        user_message.chatroom = room_ID;
                        user_message.userID = user.id_User;
                        List<string> listMessage = new List<string>();
                        listMessage.Add("Welcome " + model.NickName);
                        ViewBag.listMessage = listMessage;                        

                        return View("chatRoom", user_message);
                    }
                }
                else
                {
                    Users user = new Users();
                    var count_users = db.Users.Count();
                    count_users++;


                    user.id_User = count_users;
                    user.NickName = model.NickName;
                    user.Active = 1;
                    user.Date = DateTime.Now;

                    db.Users.Add(user);
                    db.SaveChanges();

                    User_Message user_message = new User_Message();
                    user_message.chatroom = room_ID;
                    user_message.userID = user.id_User;
                    List<string> listMessage = new List<string>();
                    listMessage.Add("Welcome " + model.NickName);
                    ViewBag.listMessage = listMessage;

                    return View("chatRoom", user_message);
                }
            }           
        }

        public IActionResult NickName(int room_ID)
        {
            ViewBag.room_ID = room_ID;
            return View();
        }

        public IActionResult MessageSend(User_Message model, int room_ID, int user_ID)
        {
            using (UsersContext db = new UsersContext())
            {
                //connect();
                Socket listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint connect = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6400);
                listen.Connect(connect);//Conection with server              

                byte[] enviar_info = new byte[100];
                string data;

                data = model.sentMessage;
                enviar_info = Encoding.Default.GetBytes(data);
                listen.Send(enviar_info);

                Users user = new Users();
                message message = new message();

                var count_message = db.message.Count();
                count_message++;

                message.id_message = count_message;
                message.id_User = user_ID;
                message.id_Room = room_ID;                
                message.messageText = model.sentMessage;
                message.msnDate = DateTime.Now;

                db.message.Add(message);
                db.SaveChanges();

                User_Message user_Message = new User_Message();

                user_Message.userID = user_ID;
                user_Message.chatroom = room_ID;

                var messages = db.message.Where(m => m.id_User == user_ID).ToList();
                List<string> listMessage = new List<string>();
                foreach (var msg in messages)
                {
                    listMessage.Add(msg.messageText);
                }
                ViewBag.listMessage = listMessage;

                return View("chatRoom", user_Message);
            }
        }

        public IActionResult createdChatRoom(ChatRooms model)
        {           
            if(model.roomName != null)
            {
                using (UsersContext db = new UsersContext())
                {
                    ChatRooms chatroom = new ChatRooms();
                    var count_rooms = db.ChatRooms.Count();
                    count_rooms++;

                    chatroom.id_Room = count_rooms;
                    chatroom.roomName = model.roomName;
                    chatroom.deleteRoom = 0;
                    chatroom.created_at = DateTime.Now;

                    db.ChatRooms.Add(chatroom);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View("CreateChatRoom");
            }            
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
