﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpGet]
        [Route("api/Category/All")]
        public List<Category> GetAllCategories()
        {
            Category c = new Category();
            return c.GetAllCategories();
        }
        
    }
}