using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EventsController : ApiController
    {
        //test
        [HttpGet]
        [Route("api/Events/test")]
        public string GetTest()
        {
            return "jjhkjh";
        }

        //**********Get***************

        //all events
        [HttpGet]
        [Route("api/Events/All/{userId}/{neiName}")]
        public List<Event> GetAllNeiEvents( int userId, string neiName)
        {
            Event e = new Event();
            return e.GetAllNeiEvents(neiName, userId);
        }

        //events the user goes to 
        [HttpGet]
        [Route("api/Events/Att")]
        public List<Event> GetAttends(int userId)
        {
            Event e = new Event();
            return e.GetAttends(userId);
        }

        //events the user created
        [HttpGet]
        [Route("api/Events/My")]
        public List<Event> GetMyEvents(int userId)
        {
            Event e = new Event();
            return e.GetMyEvents(userId);
        }

      
        //**********Post***************
        //update event - user attendance
        [HttpPost]
        [Route("api/Events/PostAtt")]
        public int PostEventAtt([FromBody] Event e)
        {
            return e.PostEventAtt(e.Id, e.Attandance[0].UserId);
        }

        [HttpPost]
        [Route("api/Events/New")]
        public int PostNewEvent([FromBody] Event e)
        {
            return e.PostNewEvent(e);
        }


        //**********Delete***************
        //update event - user attendance
        [HttpDelete]
        [Route("api/Events/DeleteAtt")]
        public int CancelEventAtt([FromBody] Event e)
        {
            return e.CancelEventAtt(e.Id, e.Attandance[0].UserId);
        }

        //**********Put***************
        //update event - user attendance
        [HttpPut]
        [Route("api/Events/Update")]
        public int UpdateEvent([FromBody] Event e)
        {
            return e.UpdateEvent(e);
        }


    }
}