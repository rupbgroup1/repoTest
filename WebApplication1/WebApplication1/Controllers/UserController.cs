using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : ApiController
    {
        

        [HttpGet]
        [Route("api/User")]
        public int Get(string username)
        {
            User userDetails = new User();
            return userDetails.GetUserByEmail(username);
        }

        // POST api/User
        [HttpPost]
        [Route("api/User")]
        public int Post([FromBody]User value)
        {
            User newUser = new User();
            return newUser.addToDB(value);
        }

        [HttpPost]
        [Route("api/User/login")]
        public User PostLogin([FromBody]User value)
        {
            User log = new User();
            return log.getUserByDetails(value);
        }

        [HttpPut]
        [Route("api/User/Extra")]
        public int Put([FromBody]User value)
        {
            User uExtra = new User();
            return uExtra.updateUserExtraDetails(value);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}