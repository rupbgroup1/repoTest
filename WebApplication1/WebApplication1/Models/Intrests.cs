using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class Intrests
    {
        int id;
        string subintrest;
        string mainInterest;
        string icon;

        public Intrests()
        {
        }

        public int Id { get => id; set => id = value; }
        public string Subintrest { get => subintrest; set => subintrest = value; }
        public string MainInterest { get => mainInterest; set => mainInterest = value; }
        public string Icon { get => icon; set => icon = value; }

        //returns all main Interests
        public List<Intrests> GetAllIntrests(){
            DBservices dbs = new DBservices();
            return dbs.GetAllIntrests();
        }

        //returns sub Interests
        public List<Intrests> GetAllSubIntrests(string main)
        {
            DBservices dbs = new DBservices();
            return dbs.GetSubIntrests(main);
        }
        
    }
}