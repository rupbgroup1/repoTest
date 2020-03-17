using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class SubCategory
    {
        int id;
        string subName;

        public SubCategory(int id, string subName)
        {
            Id = id;
            SubName = subName;
        }

        public int Id { get => id; set => id = value; }
        public string SubName { get => subName; set => subName = value; }
    }
}