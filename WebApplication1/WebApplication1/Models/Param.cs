using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class Param
    {
        int paramCode;
        string paramName;
        double paramValue;
        int paramVotes;
        string paramNameHeb;

        public int ParamCode { get => paramCode; set => paramCode = value; }
        public string ParamName { get => paramName; set => paramName = value; }
        public double ParamValue { get => paramValue; set => paramValue = value; }
        public int ParamVotes { get => paramVotes; set => paramVotes = value; }
        public string ParamNameHeb { get => paramNameHeb; set => paramNameHeb = value; }

        public Param(int paramCode, string paramName, double paramValue, int paramVotes, string paramNameHeb)
        {
            ParamCode = paramCode;
            ParamName = paramName;
            ParamValue = paramValue;
            ParamVotes = paramVotes;
            ParamNameHeb = paramNameHeb;


        }
        public Param()
        {

        }

        public List<Param> getAllParams()
        {
            DBservices dbs = new DBservices();
            return dbs.getAllParams();
        }
    }
}