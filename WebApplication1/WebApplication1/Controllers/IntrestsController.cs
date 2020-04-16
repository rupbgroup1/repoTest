using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class IntrestsController : ApiController
    {
        // GET Main interests
        [HttpGet]
        [Route("api/Intrests")]
        public List<Intrests> Get()
        {
            Intrests intrest = new Intrests();
            return intrest.GetAllIntrests();
        }
        // GET Sub interests
        [HttpGet]
        [Route("api/Intrests/Sub")]
        public List<Intrests> Get(string mainI)
        {
            Intrests intrest = new Intrests();
            return intrest.GetAllSubIntrests(mainI);
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