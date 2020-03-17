using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Losts
    {
        int id;
        string title;
        string description;
        int imageId;
        string location;
        DateTime time;
        bool status;
        User whoFound;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public int ImageId { get => imageId; set => imageId = value; }
        public string Location { get => location; set => location = value; }
        public DateTime Time { get => time; set => time = value; }
        public bool Status { get => status; set => status = value; }
        public User WhoFound { get => whoFound; set => whoFound = value; }

        public Losts(int id, string title, string description, int imageId, string location, DateTime time, bool status, User whoFound)
        {
            Id = id;
            Title = title;
            Description = description;
            ImageId = imageId;
            Location = location;
            Time = time;
            Status = status;
            WhoFound = whoFound;
        }

    }
}