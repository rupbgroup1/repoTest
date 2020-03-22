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
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/username
        //public User Get(string username, string password)
        //{
        //    User userDetails = new User();
        //    return userDetails.getUserByDetails(username, password);
        //}

        // POST api/User
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

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}