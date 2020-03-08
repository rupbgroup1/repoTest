using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}