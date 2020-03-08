using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Neighborhood
    {
        int nCode;
        string name;

        
        public int NCode { get => nCode; set => nCode = value; }
        public string Name { get => name; set => name = value; }

        public Neighborhood(int nCode, string name)
        {
            NCode = nCode;
            Name = name;
        }

    }
}