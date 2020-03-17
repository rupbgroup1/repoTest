using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Intrests
    {
        int id;
        string intrestName;

        public Intrests(int id, string intrestName)
        {
            Id = id;
            IntrestName = intrestName;
        }

        public int Id { get => id; set => id = value; }
        public string IntrestName { get => intrestName; set => intrestName = value; }
    }
}