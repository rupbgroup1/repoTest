using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class Address
    {
        int streetCode;
        string streetName;
        int streetNum;
        Neighborhood neighborhoodCode;
        City cityCode;
        double lon;
        double lat;

        public int StreetCode { get => streetCode; set => streetCode = value; }
        public string StreetName { get => streetName; set => streetName = value; }
        public int StreetNum { get => streetNum; set => streetNum = value; }
        public Neighborhood NeighborhoodCode { get => neighborhoodCode; set => neighborhoodCode = value; }
        public City CityCode { get => cityCode; set => cityCode = value; }
        public double Lon { get => lon; set => lon = value; }
        public double Lat { get => lat; set => lat = value; }

        public Address(int streetCode, string streetName, int streetNum, Neighborhood neighborhoodCode, City cityCode, double lon, double lat)
        {
            StreetCode = streetCode;
            StreetName = streetName;
            NeighborhoodCode = neighborhoodCode;
            CityCode = cityCode;
            Lon = lon;
            Lat = lat;
        }
        public Address()
        {

        }

        //public List<Address> getAllStreets(City c)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.getAllStreets(c);
        //}
    }
}