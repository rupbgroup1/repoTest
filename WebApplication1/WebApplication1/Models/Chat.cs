using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Chat
    {
        int messageCode;
        DateTime time;
        User sentFrom;
        User sentTo;

        public Chat(int messageCode, DateTime time, User sentFrom, User sentTo)
        {
            MessageCode = messageCode;
            Time = time;
            SentFrom = sentFrom;
            SentTo = sentTo;
        }

        public int MessageCode { get => messageCode; set => messageCode = value; }
        public DateTime Time { get => time; set => time = value; }
        public User SentFrom { get => sentFrom; set => sentFrom = value; }
        public User SentTo { get => sentTo; set => sentTo = value; }
    }
}