using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ServicesController : ApiController
    {
        //**********Get***************

        //all Services
        [HttpGet]
        [Route("api/Services/All/{userId}/{neiName}")]
        public List<Service> GetAllNeiServices(int userId, string neiName)
        {
            Service s = new Service();
            return s.GetAllNeiServices(neiName, userId);
        }
        
        //events the user created
        [HttpGet]
        [Route("api/Services/My")]
        public List<Service> GetMyService(int userId)
        {
            Service s = new Service();
            return s.GetMyService(userId);
        }


        //**********Post***************
        
        [HttpPost]
        [Route("api/Services/New")]
        public int PostNewService([FromBody] Service s)
        {
            return s.PostNewService(s);
        }
        
        //**********Put***************
        //update service - edit
        [HttpPut]
        [Route("api/Services/Update")]
        public int UpdateServices([FromBody] Service s)
        {
            return s.UpdateServices(s);
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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