using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class Losts
    {
        int id;
        string title;
        string description;
        string imageId;
        string location;
        string foundDate;
        bool status;
        int whoFound;
        string neighboorhoodName;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public string ImageId { get => imageId; set => imageId = value; }
        public string Location { get => location; set => location = value; }
        public string FoundDate { get => foundDate; set => foundDate = value; }
        public bool Status { get => status; set => status = value; }
        public int WhoFound { get => whoFound; set => whoFound = value; }
        public string NeighboorhoodName { get => neighboorhoodName; set => neighboorhoodName = value; }

        public Losts()
        {
        }


        public List<Losts> GetAllLosts(string neiName)
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllNeiLosts(neiName);
        }
        
        public List<Losts> GetMyLosts(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.GetMyLosts(id);
        }

        public int PostNewLosts(Losts l)
        {
            DBservices dbs = new DBservices();
            return dbs.PostNewLosts(l);
        }

        public int UpdateLosts(Losts l)
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateLosts(l);
        }


    }
}