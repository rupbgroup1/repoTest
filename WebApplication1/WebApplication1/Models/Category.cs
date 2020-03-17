using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Category
    {
        int categoryId;
        string categoryName;
        SubCategory[] subCategories;

        public Category(int categoryId, string categoryName, SubCategory[] subCategories)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            SubCategories = subCategories;
        }

        public int CategoryId { get => categoryId; set => categoryId = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public SubCategory[] SubCategories { get => subCategories; set => subCategories = value; }
    }
}