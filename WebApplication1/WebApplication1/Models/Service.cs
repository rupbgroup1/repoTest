using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class Service
    {
        int serviceId;
        string serviceName;
        string imageGallery;
        int rate;
        string description;
        string serviceAddress;
        double lat;
        double lan;
        int owner;
        string openDays;
        string openHoursStart;
        string openHoursEnds;
        Event[] events;
        int categories;
        string neighborhoodId;

        public Service()
        {
        }

        public int ServiceId { get => serviceId; set => serviceId = value; }
        public string ServiceName { get => serviceName; set => serviceName = value; }
        public string ImageGallery { get => imageGallery; set => imageGallery = value; }
        public int Rate { get => rate; set => rate = value; }
        public string Description { get => description; set => description = value; }
        public string ServiceAddress { get => serviceAddress; set => serviceAddress = value; }
        public int Owner { get => owner; set => owner = value; }
        public string OpenDays { get => openDays; set => openDays = value; }
        public string OpenHoursStart { get => openHoursStart; set => openHoursStart = value; }
        public string OpenHoursEnds { get => openHoursEnds; set => openHoursEnds = value; }
        public Event[] Events { get => events; set => events = value; }
        public int Categories { get => categories; set => categories = value; }
        public double Lat { get => lat; set => lat = value; }
        public double Lan { get => lan; set => lan = value; }
        public string NeighborhoodId { get => neighborhoodId; set => neighborhoodId = value; }

        public List<Service> GetAllNeiServices(string neiName)
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllNeiServices(neiName);
        }

        
        public List<Service> GetMyService(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.GetMyServices(id);
        }
        


        public int PostNewService(Service s)
        {
            DBservices dbs = new DBservices();
            return dbs.PostNewService(s);
        }


        public int UpdateServices(Service s)
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateService(s);
        }

    }
}