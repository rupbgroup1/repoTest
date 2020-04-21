using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class City
    {
        int cityCode;
        string cityName;
        int size;
        
        public int CityCode { get => cityCode; set => cityCode = value; }
        public string CityName { get => cityName; set => cityName = value; }
        public int Size { get => size; set => size = value; }

        public City(int cityCode, string cityName, int size)
        {
            CityCode = cityCode;
            CityName = cityName;
            Size = size;
        }
        public City()
        {

        }

        public List<City> getAllCities()
        {
            DBservices dbs = new DBservices();
            return dbs.getAllCities();
        }
    }
}