using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Models;

namespace WebApplication1
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //code for timer
        static Timer timer = new Timer();
        string path = null;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //===Once a week
            timer.Interval = 650000000;
            timer.Elapsed += tm_Tick;
            path = Server.MapPath("/");
        }

        //code for timer
        private void tm_Tick(object sender, ElapsedEventArgs e)
        {
            EndTimer();
            startSP();
            StartTimer();
        }

        private int startSP()
        {
            Votes v = new Votes();
            return v.UpdateParamsValue();
        }
        private int updateRate()
        {
            Service s = new Service();
            return s.UpdateRate();
        }

        //code for timer
        public static void StartTimer()
        {
            timer.Enabled = true;
        }

        public static void EndTimer()
        {
            timer.Enabled = false;

        }

    }
}

