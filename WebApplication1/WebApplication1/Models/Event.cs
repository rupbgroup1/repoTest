using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class Event
    {
        int id;
        string name;
        string desc;
        string startDate;
        string endDate;
        int numOfParticipants;
        string image;
        int price;
        int openedBy;
        int fromAge;
        int toAge;
        int gender;
        Service service;
        double lat;
        double lan;
        Intrests[] intrests;
        User[] attandance;
        User admin;
        int categoryId;
        int attend;
        string startHour;
        string endHour;
        string location;
        string neiCode;
        int numOfAttendance;

        public Event()
        {
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Desc { get => desc; set => desc = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string EndDate { get => endDate; set => endDate = value; }
        public string StartHour { get => startHour; set => startHour = value; }
        public string EndHour { get => endHour; set => endHour = value; }
        public int NumOfParticipants { get => numOfParticipants; set => numOfParticipants = value; }
        public string Image { get => image; set => image = value; }
        public int Price { get => price; set => price = value; }
        public int OpenedBy { get => openedBy; set => openedBy = value; }
        public int FromAge { get => fromAge; set => fromAge = value; }
        public int ToAge { get => toAge; set => toAge = value; }
        public int Gender { get => gender; set => gender = value; }
        public Service Service { get => service; set => service = value; }
        public Intrests[] Intrests { get => intrests; set => intrests = value; }
        public User[] Attandance { get => attandance; set => attandance = value; }
        public User Admin { get => admin; set => admin = value; }
        public double Lat { get => lat; set => lat = value; }
        public double Lan { get => lan; set => lan = value; }
        public int CategoryId { get => categoryId; set => categoryId = value; }
        public int Attend { get => attend; set => attend = value; }
        public string Location { get => location; set => location = value; }
        public string NeiCode { get => neiCode; set => neiCode = value; }
        public int NumOfAttendance { get => numOfAttendance; set => numOfAttendance = value; }

        public List<Event> GetAllNeiEvents(string neiName, int userId)
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllNeiEvents(neiName, userId);
        }


        public List<Event> GetAttends(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.GetAttends(id);
        }

        public List<Event> GetMyEvents(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.GetMyEvents(id);
        }


        public int PostEventAtt(int eventId, int userId)
        {
            DBservices dbs = new DBservices();
            return dbs.PostEventAtt(eventId, userId);
        }

        public int CancelEventAtt(int eventId, int userId)
        {
            DBservices dbs = new DBservices();
            return dbs.CancelEventAtt(eventId, userId);
        }


        public int PostNewEvent(Event e)
        {
            DBservices dbs = new DBservices();
            return dbs.PostNewEvent(e);
        }


        public int UpdateEvent(Event e)
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateEvent(e);
        }

    }
}