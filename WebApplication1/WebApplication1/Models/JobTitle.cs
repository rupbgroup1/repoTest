using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class JobTitle
    {
        int jobCode;
        string jobName;

        public JobTitle(int jobCode, string jobName)
        {
            JobCode = jobCode;
            JobName = jobName;
        }
        public JobTitle()
        {

        }

        public int JobCode { get => jobCode; set => jobCode = value; }
        public string JobName { get => jobName; set => jobName = value; }

        
        //returns ALL JT
        public List<JobTitle> GetAllJT()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllJT();
        }
    }
}