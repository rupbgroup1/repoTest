using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Service
    {
        int serviceId;
        string serviceName;
        Image[] imageGallery;
        int rate;
        string description;
        Address serviceAddress;
        User owner;
        string openDays;
        DateTime openHoursStart;
        DateTime openHoursEnds;
        Event[] events;
        Category[] categories;

        public Service(int serviceId, string serviceName, Image[] imageGallery, int rate, string description, Address serviceAddress, User owner, string openDays, DateTime openHoursStart, DateTime openHoursEnds, Event[] events, Category[] categories)
        {
            ServiceId = serviceId;
            ServiceName = serviceName;
            ImageGallery = imageGallery;
            Rate = rate;
            Description = description;
            ServiceAddress = serviceAddress;
            Owner = owner;
            OpenDays = openDays;
            OpenHoursStart = openHoursStart;
            OpenHoursEnds = openHoursEnds;
            Events = events;
            Categories = categories;
        }

        public int ServiceId { get => serviceId; set => serviceId = value; }
        public string ServiceName { get => serviceName; set => serviceName = value; }
        public Image[] ImageGallery { get => imageGallery; set => imageGallery = value; }
        public int Rate { get => rate; set => rate = value; }
        public string Description { get => description; set => description = value; }
        public Address ServiceAddress { get => serviceAddress; set => serviceAddress = value; }
        public User Owner { get => owner; set => owner = value; }
        public string OpenDays { get => openDays; set => openDays = value; }
        public DateTime OpenHoursStart { get => openHoursStart; set => openHoursStart = value; }
        public DateTime OpenHoursEnds { get => openHoursEnds; set => openHoursEnds = value; }
        public Event[] Events { get => events; set => events = value; }
        public Category[] Categories { get => categories; set => categories = value; }
    }
}