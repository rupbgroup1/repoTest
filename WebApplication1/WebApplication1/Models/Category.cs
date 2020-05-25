using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class Category
    {
        int categoryId;
        string categoryName;


        public int CategoryId { get => categoryId; set => categoryId = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }

        public Category()
        {
        }
        public List<Category> GetAllCategories()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllCategories();
        }
    }
}