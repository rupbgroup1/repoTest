using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class SubCategory
    {
        int id;
        string name;
        string icon;

        public SubCategory()
        {
        }

        public List<SubCategory> GetAllCategories()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllSCategories();
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Icon { get => icon; set => icon = value; }
    }
}