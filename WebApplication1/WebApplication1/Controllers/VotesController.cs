﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class VotesController : ApiController
    {
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
        [HttpPut]
        [Route("api/Votes")]
        public int Put( [FromBody]Votes value)
        {
            Votes v = new Votes();
           return v.AddNewVoteToDB(value.CategoryId);
        }

        [HttpPut]
        [Route("api/Votes/Update")]
        public int Put()
        {
            Votes v = new Votes();
            return v.UpdateParamsValue();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}