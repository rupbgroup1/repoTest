using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class MatchingCalculate
    {
//        שנת לידה 15%
//מצב משפחתי 15%
//תחומי עניין 25%
//מספר ילדים 10%
//שנת לידה ילדים 15%
//תחום עבודה 10%
//מקום עבודה 15%
        public List<User> getAllUsersInNei(string neiName, int userId)
        {
            DBservices dbs = new DBservices();
            return dbs.getAllUsersInNei(neiName, userId);
        }
    }
}