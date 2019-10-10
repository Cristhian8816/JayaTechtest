using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jayatechTest.Models;

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
                         id_User = chatroom.id_User,
                         roomName = chatroom.roomName,
                         deleteRoom = chatroom.deleteRoom,
                         created_at = chatroom.created_at,
                     }
                    );
            return View(model);            
        }

        public IActionResult procesNickName(Users model)
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

                        return RedirectToAction("ChatRoom");
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

                    return RedirectToAction("ChatRoom");
                }
            }           
        }

        public IActionResult NickName()
        {         
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
