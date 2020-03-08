﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public int JobCode { get => jobCode; set => jobCode = value; }
        public string JobName { get => jobName; set => jobName = value; }
    }
}