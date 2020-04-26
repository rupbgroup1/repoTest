using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class TimerController : ApiController
    {
        //code for timer
        [HttpGet]
        [Route("api/Timer/start")]
        public void StartTimer()
        {
            WebApiApplication.StartTimer();
        }

        //code for timer
        [HttpGet]
        [Route("api/Timer/stop")]
        public void StopTimer()
        {
            WebApiApplication.EndTimer();
        }
    }
}