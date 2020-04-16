using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class NeighboorsController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        [Route("api/Neighboors/userName")]
        public List<User> GetUsersByName([FromBody]User u)
        {
            User user = new User();
            if (u.FirstName.Contains(' ')) {
                string[] fullName = u.FirstName.Split(' ');
                return user.GetAllUsersByfullName(u.NeighborhoodName, fullName[0], fullName[1]);
                
            }
            else return user.GetAllUsersByName(u.NeighborhoodName, u.FirstName);
        }

        [HttpGet]
        [Route("api/Neighboors/Intrest/{NeighborhoodName}/{intrestId}")]
        //[Route("api/YOURCONTROLLER/{paramOne}/{paramTwo}")]
        public List<User> GetUsersByIntrest(string NeighborhoodName, int intrestId)
        {
            User user = new User();
            return user.GetUsersByIntrest(NeighborhoodName, intrestId);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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