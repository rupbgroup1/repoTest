using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Kids
    {
        int id;
        int yearOfBirth;
        

        public int Id { get => id; set => id = value; }
        public int YearOfBirth { get => yearOfBirth; set => yearOfBirth = value; }

        public Kids(int id, User parent, int yearOfBirth)
        {
            Id = id;
            YearOfBirth = yearOfBirth;
        }
    }
}