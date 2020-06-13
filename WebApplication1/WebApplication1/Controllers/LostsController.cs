using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LostsController : ApiController
    {
        //**********Get***************

        //all Services
        [HttpGet]
        [Route("api/Losts/All")]
        public List<Losts> GetAllLosts(string neiName)
        {
            Losts s = new Losts();
            return s.GetAllLosts(neiName);
        }

        //events the user created
        [HttpGet]
        [Route("api/Losts/My")]
        public List<Losts> GetMyLosts(int userId)
        {
            Losts s = new Losts();
            return s.GetMyLosts(userId);
        }


        //**********Post***************

        [HttpPost]
        [Route("api/Losts/New")]
        public int PostNewLosts([FromBody] Losts s)
        {
            return s.PostNewLosts(s);
        }

        //**********Put***************
        [HttpPut]
        [Route("api/Losts/Update")]
        public int UpdateLosts([FromBody] Losts s)
        {
            return s.UpdateLosts(s);
        }
        
    }
}