using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class Neighborhood
    {
        string nCode;
        string name;

        
        public string NCode { get => nCode; set => nCode = value; }
        public string Name { get => name; set => name = value; }

        public Neighborhood(string nCode, string name)
        {
            NCode = nCode;
            Name = name;
        }

        public Neighborhood()
        {
        }

        public List<Neighborhood> getAllNeiInCity(string cityName)
        {
            DBservices dbs = new DBservices();
             return dbs.getAllNeiInCity(cityName);
        }
    }
}