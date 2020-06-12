using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SubCategoryController : ApiController
    {
        [HttpGet]
        [Route("api/SubCategory/All")]
        public List<SubCategory> GetAllCategories()
        {
            SubCategory c = new SubCategory();
            return c.GetAllCategories();
        }
    }
}