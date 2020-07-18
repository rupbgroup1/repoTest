using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class Chat
    {
        int fromUser;
        int toUser;

        public int FromUser { get => fromUser; set => fromUser = value; }
        public int ToUser { get => toUser; set => toUser = value; }

        public Chat()
        {

        }

        public int postNewChat(Chat c)
        {
            DBservices dbs = new DBservices();
            return dbs.postNewChat(c);
        }
    }
}