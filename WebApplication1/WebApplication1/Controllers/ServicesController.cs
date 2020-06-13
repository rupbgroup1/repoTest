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
        [Route("api/Services/All")]
        public List<Service> GetAllNeiServices(string neiName)
        {
            Service s = new Service();
            return s.GetAllNeiServices(neiName);
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

        //update service - Rate
        [HttpPut]
        [Route("api/Services/UpdateRate/{serviceId}/{serviceRate}")]
        public int UpdateServiceRate(int serviceId, int serviceRate)
        {
            Service s = new Service();
            return s.UpdateServiceRate(serviceId, serviceRate);
        }


    }
}