using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Event
    {
        int id;
        string name;
        string desc;
        DateTime startDate;
        DateTime endDate;
        int numOfParticipants;
        Image image;
        int price;
        User openedBy;
        int fromAge;
        int toAge;
        int gender;
        Service service;
        string location;
        Intrests[] intrests;
        User[] attandance;
        User admin;

        public Event(int id, string name, string desc, DateTime startDate, DateTime endDate, int numOfParticipants, Image image, int price, User openedBy, int fromAge, int toAge, int gender, Service service, string location, Intrests[] intrests, User[] attandance, User admin)
        {
            Id = id;
            Name = name;
            Desc = desc;
            StartDate = startDate;
            EndDate = endDate;
            NumOfParticipants = numOfParticipants;
            Image = image;
            Price = price;
            OpenedBy = openedBy;
            FromAge = fromAge;
            ToAge = toAge;
            Gender = gender;
            Service = service;
            Location = location;
            Intrests = intrests;
            Attandance = attandance;
            Admin = admin;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Desc { get => desc; set => desc = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public int NumOfParticipants { get => numOfParticipants; set => numOfParticipants = value; }
        public Image Image { get => image; set => image = value; }
        public int Price { get => price; set => price = value; }
        public User OpenedBy { get => openedBy; set => openedBy = value; }
        public int FromAge { get => fromAge; set => fromAge = value; }
        public int ToAge { get => toAge; set => toAge = value; }
        public int Gender { get => gender; set => gender = value; }
        public Service Service { get => service; set => service = value; }
        public string Location { get => location; set => location = value; }
        public Intrests[] Intrests { get => intrests; set => intrests = value; }
        public User[] Attandance { get => attandance; set => attandance = value; }
        public User Admin { get => admin; set => admin = value; }
    }
}