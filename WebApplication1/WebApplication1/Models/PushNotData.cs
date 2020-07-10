using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PushNotData
    {
        public string to { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public int badge { get; set; }
        public Data data { get; set; }
    }
}