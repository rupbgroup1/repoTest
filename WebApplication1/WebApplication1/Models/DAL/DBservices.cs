using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace WebApplication1.Models.DAL
{
    public class DBservices
    {

        public SqlDataAdapter da;
        public DataTable dt;

        public DBservices()
        {
        }

        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandType = CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }

        //*****************Category***************************************

        public List<Category> GetAllCategories()
        {
            List<Category> categoriesList = new List<Category>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Maincategory";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Category c = new Category();
                    c.CategoryId = Convert.ToInt32(dr["IdMainCategory"]);
                    c.CategoryName = (string)dr["NameMainCategory"];

                    categoriesList.Add(c);
                }

                return categoriesList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }



        public List<SubCategory> GetAllSCategories()
        {
            List<SubCategory> sCategoriesList = new List<SubCategory>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from BusinessCat";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    SubCategory c = new SubCategory();
                    c.Id = Convert.ToInt32(dr["ID"]);
                    c.Name = (string)dr["CatName"];
                    c.Icon = (string)dr["CatIcon"];

                    sCategoriesList.Add(c);
                }

                return sCategoriesList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        //*****************Events***************************************

        //all nei's events
        public List<Event> GetAllNeiEvents(string neiName, int userId)
        {
            List<Event> eventsList = new List<Event>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = " select E.EventCode, EventName,EventDescription, StartDate,EndDate, NumOfParticipants,ImageLink, price, FromAge,ToAge, E.Gender, LocationLat, LocationLan, Location, OpenByUserCode, Category, FirstName, LastName, U.UserCode, A.EventCode as attend from EventsTable E left join Users U on E.OpenByUserCode=U.UserCode left join (select EventCode from EventsAttendance where UserCode=" + userId + " ) A on E.EventCode=A.EventCode where E.NeighborhoodName = '" + neiName + "' and E.EndDate >= GETDATE() Order by E.StartDate";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Event e = new Event();
                    e.Id = Convert.ToInt32(dr["EventCode"]);
                    e.Name = (string)dr["EventName"];
                    e.Desc = (string)dr["EventDescription"];
                    e.StartDate = Convert.ToString(dr["StartDate"]);
                    e.EndDate = Convert.ToString(dr["EndDate"]);
                    e.StartHour = Convert.ToString(dr["StartDate"]);
                    e.EndHour = Convert.ToString(dr["EndDate"]);
                    e.NumOfParticipants = Convert.ToInt32(dr["NumOfParticipants"]);
                    if (dr["ImageLink"].GetType() != typeof(DBNull))
                    {
                        e.Image = (string)dr["ImageLink"];
                    }
                    e.Price = Convert.ToInt32(dr["Price"]);
                    e.FromAge = Convert.ToInt32(dr["FromAge"]);
                    e.ToAge = Convert.ToInt32(dr["ToAge"]);
                    if (dr["Gender"].GetType() != typeof(DBNull))
                    {
                        e.Gender = Convert.ToInt32(dr["Gender"]);
                    }
                    if (dr["LocationLat"].GetType() != typeof(DBNull))
                    {
                        e.Lat = Convert.ToDouble(dr["LocationLat"]);
                    }
                    if (dr["LocationLan"].GetType() != typeof(DBNull))
                    {
                        e.Lan = Convert.ToDouble(dr["LocationLan"]);
                    }
                    if (dr["Location"].GetType() != typeof(DBNull))
                    {
                        e.Location = (string)dr["Location"];
                    }
                    e.CategoryId = Convert.ToInt32(dr["Category"]);
                    User u = new User();
                    u.UserId = Convert.ToInt32(dr["UserCode"]);
                    u.FirstName = (string)dr["FirstName"];
                    u.LastName = (string)dr["LastName"];
                    e.Admin = u;
                    if (dr["attend"].GetType() != typeof(DBNull))
                    {
                        e.Attend = 1;

                    }
                    eventsList.Add(e);
                }

                return eventsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //events the user attends to
        public List<Event> GetAttends(int userId)
        {
            List<Event> eventsList = new List<Event>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "  select DISTINCT E.EventCode, EventName,EventDescription, StartDate,EndDate, NumOfParticipants,ImageLink, price, FromAge,ToAge, E.Gender, LocationLat, LocationLan, Location, OpenByUserCode, Category, U.FirstName, U.LastName from EventsAttendance A left join EventsTable E on A.EventCode = E.EventCode left join Users U on E.OpenByUserCode=U.UserCode where A.UserCode=" + userId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Event e = new Event();
                    e.Id = Convert.ToInt32(dr["EventCode"]);
                    e.Name = (string)dr["EventName"];
                    e.Desc = (string)dr["EventDescription"];
                    e.StartDate = Convert.ToString(dr["StartDate"]);
                    e.EndDate = Convert.ToString(dr["EndDate"]);
                    e.StartHour = Convert.ToString(dr["StartDate"]);
                    e.EndHour = Convert.ToString(dr["EndDate"]);
                    e.NumOfParticipants = Convert.ToInt32(dr["NumOfParticipants"]);
                    if (dr["ImageLink"].GetType() != typeof(DBNull))
                    {
                        e.Image = (string)dr["ImageLink"];
                    }
                    e.Price = Convert.ToInt32(dr["Price"]);
                    e.FromAge = Convert.ToInt32(dr["FromAge"]);
                    e.ToAge = Convert.ToInt32(dr["ToAge"]);
                    if (dr["Gender"].GetType() != typeof(DBNull))
                    {
                        e.Gender = Convert.ToInt32(dr["Gender"]);
                    }
                    if (dr["LocationLat"].GetType() != typeof(DBNull))
                    {
                        e.Lat = Convert.ToDouble(dr["LocationLat"]);
                    }
                    if (dr["LocationLan"].GetType() != typeof(DBNull))
                    {
                        e.Lan = Convert.ToDouble(dr["LocationLan"]);
                    }
                    if (dr["Location"].GetType() != typeof(DBNull))
                    {
                        e.Location = (string)dr["Location"];
                    }
                    e.Price = Convert.ToInt32(dr["Price"]);
                    e.FromAge = Convert.ToInt32(dr["FromAge"]);
                    e.ToAge = Convert.ToInt32(dr["ToAge"]);
                    e.CategoryId = Convert.ToInt32(dr["Category"]);
                    User u = new User();
                    u.FirstName = (string)dr["FirstName"];
                    u.LastName = (string)dr["LastName"];
                    e.Admin = u;
                    eventsList.Add(e);
                }

                return eventsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //events the user created
        public List<Event> GetMyEvents(int userId)
        {
            List<Event> eventsList = new List<Event>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select EventCode, EventName,EventDescription, StartDate,EndDate, NumOfParticipants,ImageLink, price, FromAge,ToAge, Gender, LocationLat, LocationLan, Location, OpenByUserCode, Category from EventsTable where OpenByUserCode=" + userId + " Order by StartDate Desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Event e = new Event();
                    e.Id = Convert.ToInt32(dr["EventCode"]);
                    e.Name = (string)dr["EventName"];
                    e.Desc = (string)dr["EventDescription"];
                    e.StartDate = Convert.ToString(dr["StartDate"]);
                    e.EndDate = Convert.ToString(dr["EndDate"]);
                    e.StartHour = Convert.ToString(dr["StartDate"]);
                    e.EndHour = Convert.ToString(dr["EndDate"]);
                    e.NumOfParticipants = Convert.ToInt32(dr["NumOfParticipants"]);
                    if (dr["ImageLink"].GetType() != typeof(DBNull))
                    {
                        e.Image = (string)dr["ImageLink"];
                    }
                    e.Price = Convert.ToInt32(dr["Price"]);
                    e.FromAge = Convert.ToInt32(dr["FromAge"]);
                    e.ToAge = Convert.ToInt32(dr["ToAge"]);
                    if (dr["Gender"].GetType() != typeof(DBNull))
                    {
                        e.Gender = Convert.ToInt32(dr["Gender"]);
                    }
                    if (dr["LocationLat"].GetType() != typeof(DBNull))
                    {
                        e.Lat = Convert.ToDouble(dr["LocationLat"]);
                    }
                    if (dr["LocationLan"].GetType() != typeof(DBNull))
                    {
                        e.Lan = Convert.ToDouble(dr["LocationLan"]);
                    }
                    if (dr["Location"].GetType() != typeof(DBNull))
                    {
                        e.Location = (string)dr["Location"];
                    }
                    e.CategoryId = Convert.ToInt32(dr["Category"]);

                    eventsList.Add(e);
                }

                return eventsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }


        //add new event attend command
        private String BuildEventAttInsertCommand(int eventId, int userId)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}')", eventId, userId);
            String prefix = "INSERT INTO EventsAttendance" + "(EventCode, UserCode)";
            command = prefix + sb.ToString();
            return command;
        }

        //post new event attendance
        public int PostEventAtt(int eventId, int userId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildEventAttInsertCommand(eventId, userId);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //create new event command
        private String BuildEventInsertCommand(Event e)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            string startDate = e.StartDate.Split('T')[0] + "T" + e.StartHour.Split('T')[1];
            string endDate = e.EndDate.Split('T')[0] + "T" + e.EndHour.Split('T')[1];
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}')", e.Name, e.Desc, startDate, endDate, e.NumOfParticipants, e.Image, e.Price, e.OpenedBy, e.FromAge, e.ToAge, e.NeiCode, e.CategoryId, e.Location, e.Lat, e.Lan);
            String prefix = "INSERT INTO EventsTable" + "(EventName, EventDescription, StartDate , EndDate, NumOfParticipants, ImageLink , Price, OpenByUserCode, FromAge, ToAge, NeighborhoodName, Category, Location, LocationLat, LocationLan )";
            command = prefix + sb.ToString();
            return command;
        }

        //post new event 
        public int PostNewEvent(Event e)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildEventInsertCommand(e);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                    if (e.Intrests.Length > 0)
                        postEventInterests(e);
                }
            }
        }


        public int postEventInterests(Event e)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            //build insert query
            string values = "";
            for (int i = 0; i < e.Intrests.Length; i++)
            {
                if (i != 0)
                    values += ",";
                values += "((select top 1 EventCode from EventsTable order by EventCode desc)," + e.Intrests[i].Id + ")";
            }
            // String cStr = BuildInsertCommand(user);      // helper method to build the insert string
            String cStr = "insert into EventsAndInterests (EventCode, InterestId) values " + values;

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //cancel event attendance
        public int CancelEventAtt(int eventId, int userId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = "Delete from EventsAttendance where EventCode=" + eventId + " and UserCode=" + userId;   // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //update event - command
        private String BuildEventUpdateCommand(Event e)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            string startDate = e.StartDate.Split('T')[0] + "T" + e.StartHour.Split('T')[1];
            string endDate = e.EndDate.Split('T')[0] + "T" + e.EndHour.Split('T')[1];
            // use a string builder to create the dynamic string
            command = "update EventsTable set EventName='" + e.Name + "', EventDescription='" + e.Desc + "', StartDate='" + startDate + "', EndDate='" + endDate + "', NumOfParticipants=" + e.NumOfParticipants + ", ImageLink='" + e.Image + "', Price=" + e.Price + ", FromAge=" + e.FromAge + ", ToAge=" + e.ToAge + ", Category=" + e.CategoryId + ", Location='" + e.Location + "', LocationLat=" + e.Lat + ", LocationLan= " + e.Lan + " Where EventCode=" + e.Id;
            return command;
        }

        //update event
        public int UpdateEvent(Event e)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildEventUpdateCommand(e);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //*****************Service***************************************

        //all nei's Services
        public List<Service> GetAllNeiServices(string neiName)
        {
            List<Service> sList = new List<Service>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from ServicesTable where Neighboorhood='" + neiName + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Service s = new Service();
                    s.ServiceId = Convert.ToInt32(dr["ServiceId"]);
                    s.ServiceName = (string)dr["ServiceName"];
                    if (dr["ImagePrimary"].GetType() != typeof(DBNull))
                    {
                        s.ImageGallery = (string)dr["ImagePrimary"];
                    }
                    if (Convert.ToInt32(dr["TotalVotes"]) != 0)
                    {
                         s.Rate = Convert.ToInt32(dr["TotalRate"]) / Convert.ToInt32(dr["TotalVotes"]);
                    }
                    s.Description = (string)dr["ServiceDescription"];
                    if (dr["ServiceAddress"].GetType() != typeof(DBNull))
                    {
                        s.ServiceAddress = (string)dr["ServiceAddress"];
                    }
                    if (dr["Lan"].GetType() != typeof(DBNull))
                    {
                        s.Lan = Convert.ToDouble(dr["Lan"]);
                    }
                    if (dr["Lat"].GetType() != typeof(DBNull))
                    {
                        s.Lat = Convert.ToDouble(dr["Lat"]);
                    }
                    s.Owner = Convert.ToInt32(dr["OwnerId"]);
                    if (dr["OpenDays"].GetType() != typeof(DBNull))
                    {
                        s.OpenDays = (string)dr["OpenDays"];
                    }
                    if (dr["OpenHoursStart"].GetType() != typeof(DBNull))
                    {
                        s.OpenHoursStart = Convert.ToString(dr["OpenHoursStart"]);
                    }
                    if (dr["OpenHoursEnds"].GetType() != typeof(DBNull))
                    {
                        s.OpenHoursEnds = Convert.ToString(dr["OpenHoursEnds"]);
                    }
                    s.Categories = Convert.ToInt32(dr["Categories"]);
                    s.NeighborhoodId = (string)dr["Neighboorhood"];
                    sList.Add(s);
                }

                return sList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //services the user created
        public List<Service> GetMyServices(int userId)
        {
            List<Service> sList = new List<Service>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from ServicesTable where OwnerId=" + userId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Service s = new Service();
                    s.ServiceId = Convert.ToInt32(dr["ServiceId"]);
                    s.ServiceName = (string)dr["ServiceName"];
                    if (dr["ImagePrimary"].GetType() != typeof(DBNull))
                    {
                        s.ImageGallery = (string)dr["ImagePrimary"];
                    }
                    if (Convert.ToInt32(dr["TotalVotes"]) != 0)
                    {
                        s.Rate = Convert.ToInt32(dr["TotalRate"]) / Convert.ToInt32(dr["TotalVotes"]);
                    }
                    s.Description = (string)dr["ServiceDescription"];
                    if (dr["ServiceAddress"].GetType() != typeof(DBNull))
                    {
                        s.ServiceAddress = (string)dr["ServiceAddress"];
                    }
                    if (dr["Lan"].GetType() != typeof(DBNull))
                    {
                        s.Lan = Convert.ToDouble(dr["Lan"]);
                    }
                    if (dr["Lat"].GetType() != typeof(DBNull))
                    {
                        s.Lat = Convert.ToDouble(dr["Lat"]);
                    }
                    s.Owner = Convert.ToInt32(dr["OwnerId"]);
                    if (dr["OpenDays"].GetType() != typeof(DBNull))
                    {
                        s.OpenDays = (string)dr["OpenDays"];
                    }
                    if (dr["OpenHoursStart"].GetType() != typeof(DBNull))
                    {
                        string ohs = Convert.ToString(dr["OpenHoursStart"]);
                        s.OpenHoursStart = ohs.Substring(0, 5);
                    }
                    if (dr["OpenHoursEnds"].GetType() != typeof(DBNull))
                    {
                        string ohe = Convert.ToString(dr["OpenHoursEnds"]);
                        s.OpenHoursEnds = ohe.Substring(0, 5);
                    }
                    s.Categories = Convert.ToInt32(dr["Categories"]);
                    s.NeighborhoodId = (string)dr["Neighboorhood"];
                    sList.Add(s);
                }

                return sList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }


        //create new service command
        private String BuildServiceInsertCommand(Service s)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            //string startDate = e.StartDate.Split('T')[0] + "T" + e.StartHour.Split('T')[1];
            //string endDate = e.EndDate.Split('T')[0] + "T" + e.EndHour.Split('T')[1];
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')", s.ServiceName, s.ImageGallery, s.Description, s.ServiceAddress, s.Lat, s.Lan, s.Owner, s.OpenDays, s.OpenHoursStart, s.OpenHoursEnds, s.Categories, s.NeighborhoodId);
            String prefix = "INSERT INTO ServicesTable" + "(ServiceName, ImagePrimary , ServiceDescription, ServiceAddress, Lat , Lan, OwnerId, OpenDays, OpenHoursStart, OpenHoursEnds, Categories, Neighboorhood)";
            command = prefix + sb.ToString();
            return command;
        }

        //post new event 
        public int PostNewService(Service s)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildServiceInsertCommand(s);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }



        //update service - command
        private String BuildServiceUpdateCommand(Service s)
        {
            String command;
            StringBuilder sb = new StringBuilder();

            command = "update ServicesTable set ServiceName='" + s.ServiceName + "', ImagePrimary='" + s.ImageGallery + "', ServiceDescription='" + s.Description + "', ServiceAddress='" + s.ServiceAddress + "', Lat=" + s.Lat + ", Lan=" + s.Lan + ", OwnerId=" + s.Owner + ", OpenDays='" + s.OpenDays + "', OpenHoursStart='" + s.OpenHoursStart + "', OpenHoursEnds='" + s.OpenHoursEnds + "', Categories=" + s.Categories + ", Neighboorhood='" + s.NeighborhoodId + "' Where ServiceId=" + s.ServiceId;
            return command;
        }

        //update event
        public int UpdateService(Service s)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildServiceUpdateCommand(s);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        public int UpdateServiceRate(int id, int rate)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = "update ServicesTable set TotalRate=TotalRate+" + rate + ", TotalVotes=TotalVotes+1 where ServiceId=" + id;

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        public int UpdateRate()
        {
            SqlConnection con;
            con = connect("DBConnectionString"); // create the connection
            SqlCommand cmd = new SqlCommand("SP_calculateServiceRate", con);

            try
            {

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                // cmd.Parameters.Add(new SqlParameter("@userCode", userId));
                // execute the command
                // SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        //*********
        //****************Losts***************************************
        //*********

        //all nei's Losts
        public List<Losts> GetAllNeiLosts(string neiName)
        {
            List<Losts> lostsList = new List<Losts>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Losts where NeighboorhoodName='" + neiName + "' and LostStatus='False'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Losts lost = new Losts();
                    lost.Id = Convert.ToInt32(dr["Id"]);
                    lost.Title = (string)dr["Title"];
                    if (dr["LostDescription"].GetType() != typeof(DBNull))
                    {
                        lost.Description = (string)dr["LostDescription"];
                    }
                    if (dr["ImageId"].GetType() != typeof(DBNull))
                    {
                        lost.ImageId = (string)dr["ImageId"];
                    }
                    if (dr["LocationName"].GetType() != typeof(DBNull))
                    {
                        lost.Location = (string)dr["LocationName"];
                    }
                    if (dr["FoundDate"].GetType() != typeof(DBNull))
                    {
                        lost.FoundDate = Convert.ToString(dr["FoundDate"]);
                    }
                    lost.Status = Convert.ToBoolean(dr["LostStatus"]);
                    lost.WhoFound = Convert.ToInt32(dr["whoFound"]);
                    lostsList.Add(lost);
                }

                return lostsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }


        public List<Losts> GetMyLosts(int userId)
        {
            List<Losts> sList = new List<Losts>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Losts where whoFound=" + userId + " and LostStatus=0";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Losts lost = new Losts();
                    lost.Id = Convert.ToInt32(dr["Id"]);
                    lost.Title = (string)dr["Title"];
                    if (dr["LostDescription"].GetType() != typeof(DBNull))
                    {
                        lost.Description = (string)dr["LostDescription"];
                    }
                    if (dr["ImageId"].GetType() != typeof(DBNull))
                    {
                        lost.ImageId = (string)dr["ImageId"];
                    }
                    if (dr["LocationName"].GetType() != typeof(DBNull))
                    {
                        lost.Location = (string)dr["LocationName"];
                    }
                    if (dr["FoundDate"].GetType() != typeof(DBNull))
                    {
                        lost.FoundDate = Convert.ToString(dr["FoundDate"]);
                    }
                    lost.Status = Convert.ToBoolean(dr["LostStatus"]);
                    lost.WhoFound = Convert.ToInt32(dr["whoFound"]);
                    sList.Add(lost);
                }

                return sList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }


        //create new service command
        private String BuildLostsInsertCommand(Losts s)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", s.Title, s.Description, s.ImageId, s.Location, s.FoundDate, s.Status, s.WhoFound, s.NeighboorhoodName);
            String prefix = "INSERT INTO Losts" + "(Title, LostDescription, ImageId, LocationName, FoundDate, LostStatus, whoFound, NeighboorhoodName )";
            command = prefix + sb.ToString();
            return command;
        }

        //post new event 
        public int PostNewLosts(Losts s)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildLostsInsertCommand(s);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }



        //update service - command
        private String BuildLostsUpdateCommand(Losts s)
        {
            String command;
            StringBuilder sb = new StringBuilder();

            command = "update Losts set LostStatus = 'True' Where Id=" + s.Id;
            return command;
        }

        //update event
        public int UpdateLosts(Losts s)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildLostsUpdateCommand(s);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        //*****************User***************************************

        //add new user command
        private String BuildInsertCommand(User u)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}','{4}','{5}','{6}', '{7}','{8}','{9}','{10}' )", u.Email, u.Password, u.FirstName, u.LastName, u.Gender, u.YearOfBirth, u.ImagePath, u.Lat, u.Lan, u.CityName, u.NeighborhoodName);
            String prefix = "INSERT INTO Users" + "(Email, PasswordUser, FirstName, LastName, Gender, YearOfBirth, ImageId, Lat, Long, CityName, NeighborhoodName)";
            command = prefix + sb.ToString();

            return command;
        }

        public int addNewUserToDB(User newUser)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(newUser);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //get user by username and password - Called from login screen

        public User getUserByDetails(User u)
        {
            User User = getUserByDetailsUser(u);
            Intrests[] iArray = getUserByDetailsInterest(User);
            Kids[] kArray = getUserByDetailsKids(User);
            User.Intrests = iArray;
            User.Kids = kArray;
            return User;
        }

        public Kids[] getUserByDetailsKids(User u)
        {
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                String selectSTR = "select * from KidsAge where UserCode =" + u.UserId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                List<Kids> allKids = new List<Kids>();
                while (dr.Read())
                {
                    Kids k = new Kids();
                    k.Id = Convert.ToInt32(dr["UserCode"]);
                    k.YearOfBirth = Convert.ToInt32(dr["YearOfBirth"]);
                    allKids.Add(k);
                }
                Kids[] kArray = allKids.ToArray();

                return kArray;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }


        public Intrests[] getUserByDetailsInterest(User u)
        {
            User userDetails = new User();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                String selectSTR = " select Id,MainCat,SubCat from UsersAndIntrests UI left join Interests on UI.IntrestId = Interests.Id  where UI.UserCode =" + u.UserId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                List<Intrests> allIntrests = new List<Intrests>();
                while (dr.Read())
                {
                    Intrests interest = new Intrests();
                    interest.Id = Convert.ToInt32(dr["Id"]);
                    interest.MainInterest = (string)dr["MainCat"];
                    interest.Subintrest = (string)dr["SubCat"];

                    allIntrests.Add(interest);
                }
                Intrests[] iArray = allIntrests.ToArray();

                return iArray;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        public User getUserByDetailsUser(User u)
        {
            User userDetails = new User();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                String selectSTR = "  select *  from Users left join JobTitle on Users.JobTitleCode=JobTitle.Code where Email='" + u.Email + "' AND PasswordUser='" + u.Password + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    userDetails.UserId = Convert.ToInt32(dr["UserCode"]);
                    userDetails.FirstName = (string)dr["FirstName"];
                    userDetails.LastName = (string)dr["LastName"];
                    userDetails.Gender = Convert.ToInt32(dr["Gender"]);
                    userDetails.YearOfBirth = Convert.ToInt32(dr["YearOfBirth"]);

                    if (dr["ImageId"].GetType() != typeof(DBNull))
                    {
                        userDetails.ImagePath = (string)dr["ImageId"];
                    }
                    if (dr["JobTitleCode"].GetType() != typeof(DBNull))
                    {
                        userDetails.JobTitleId = Convert.ToInt32(dr["JobTitleCode"]);
                        int code = Convert.ToInt32(dr["Code"]);
                        string name = (string)dr["JobName"];
                        JobTitle JT = new JobTitle(code, name);
                        userDetails.JobTitle = JT;

                    }
                    if (dr["WorkPlace"].GetType() != typeof(DBNull))
                    {
                        userDetails.WorkPlace = (string)dr["WorkPlace"];
                    }
                    if (dr["FamilyStatus"].GetType() != typeof(DBNull))
                    {
                        userDetails.FamilyStatus = (string)dr["FamilyStatus"];
                    }
                    if (dr["NumberOfChildren"].GetType() != typeof(DBNull))
                    {
                        userDetails.NumOfChildren = Convert.ToInt32(dr["NumberOfChildren"]);
                    }
                    if (dr["AboutMe"].GetType() != typeof(DBNull))
                    {
                        userDetails.AboutMe = (string)dr["AboutMe"];
                    }
                    if (dr["CityName"].GetType() != typeof(DBNull))
                    {
                        userDetails.CityName = (string)dr["CityName"];
                    }
                    if (dr["Long"].GetType() != typeof(DBNull))
                    {
                        userDetails.Lan = Convert.ToDouble(dr["Long"]);
                    }
                    if (dr["Lat"].GetType() != typeof(DBNull))
                    {
                        userDetails.Lat = Convert.ToDouble(dr["Lat"]);
                    }
                    if (dr["NeighborhoodName"].GetType() != typeof(DBNull))
                    {
                        userDetails.NeighborhoodName = (string)dr["NeighborhoodName"];
                    }

                }

                return userDetails;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }


        //get - check if there is user with this email in the system

        public int GetUserByEmail(string userEmail)
        {
            SqlConnection con = null;
            int UserId = 0;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                String selectSTR = "SELECT UserCode FROM Users where Email='" + userEmail + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    UserId = Convert.ToInt32(dr["UserCode"]);
                }

                return UserId;
            }
            catch (Exception ex)
            {
                // write to log
                return 0;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        //get all details by Id
        public User getUserDetails(int Id)
        {
            User userDetails = new User();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                String selectSTR = "SELECT * FROM Users where UserCode=" + Id;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    userDetails.FirstName = (string)dr["FirstName"];
                    userDetails.LastName = (string)dr["LastName"];
                    userDetails.Gender = Convert.ToInt32(dr["Gender"]);
                    userDetails.ImagePath = (string)dr["ImageId"];
                    userDetails.JobTitleId = Convert.ToInt32(dr["JobTitleCode"]);
                    userDetails.WorkPlace = (string)dr["WorkPlace"];
                    userDetails.FamilyStatus = (string)dr["FamilyStatus"];
                    userDetails.CityName = (string)dr["CityName"];
                    userDetails.Lan = Convert.ToDouble(dr["Long"]);
                    userDetails.Lat = Convert.ToDouble(dr["Lat"]);



                }

                return userDetails;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public int updatePassword(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // String cStr = BuildInsertCommand(user);      // helper method to build the insert string
            String cStr = "Update Users set PasswordUser='" + user.Password + "' where Email='" + user.Email + "'";
            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        //***************Extra reg**********************

        public int updateUserExtraDetails(User user)
        {
            deletefromKidsTable(user);
            insertIntoKidsTable(user);
            deletefromInterestsTable(user);
            insertIntoInterestsTable(user);
            return updateUsersTable(user);
        }

        public int updateUsersTable(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // String cStr = BuildInsertCommand(user);      // helper method to build the insert string
            String cStr = "Update Users set FamilyStatus='" + user.FamilyStatus + "', JobTitleCode=" + user.JobTitleId + ", WorkPlace='" + user.WorkPlace + "', NumberOfChildren=" + user.NumOfChildren + ",AboutMe='" + user.AboutMe + "' where UserCode=" + user.UserId;
            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public int deletefromKidsTable(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // String cStr = BuildInsertCommand(user);      // helper method to build the insert string
            String cStr = "DELETE FROM KidsAge WHERE UserCode=" + user.UserId;
            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public int insertIntoKidsTable(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            //build insert query
            string values = "";
            for (int i = 0; i < user.Kids.Length; i++)
            {
                if (i != 0)
                    values += ",";
                values += "(" + user.Kids[i].Id + "," + user.Kids[i].YearOfBirth + ")";
            }
            // String cStr = BuildInsertCommand(user);      // helper method to build the insert string
            String cStr = "INSERT INTO KidsAge (UserCode, YearOfBirth) VALUES " + values;

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //Interests
        public int deletefromInterestsTable(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = "DELETE FROM UsersAndIntrests WHERE UserCode=" + user.UserId;
            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public int insertIntoInterestsTable(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            //build insert query
            string values = "";
            for (int i = 0; i < user.Intrests.Length; i++)
            {
                if (i != 0)
                    values += ",";
                values += "(" + user.UserId + "," + user.Intrests[i].Id + ")";
            }
            // String cStr = BuildInsertCommand(user);      // helper method to build the insert string
            String cStr = "INSERT INTO UsersAndIntrests (UserCode, IntrestId) VALUES " + values;

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public int updateUser(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // String cStr = BuildInsertCommand(user);      // helper method to build the insert string
            String cStr = "Update Users set FirstName='" + user.FirstName + "', LastName='" + user.LastName + "', Gender='" + user.Gender + "', YearOfBirth='" + user.YearOfBirth + "', FamilyStatus ='" + user.FamilyStatus + "', JobTitleCode=" + user.JobTitleId + ", WorkPlace='" + user.WorkPlace + "', NumberOfChildren=" + user.NumOfChildren + ",AboutMe='" + user.AboutMe + "' where UserCode=" + user.UserId;
            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        //*****************Neighboors***************************

        //filter neighboors by search *full* name
        public List<User> GetAllUsersByfullName(string neiName, string firstName, string lastName)
        {
            List<User> userByNameList = new List<User>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT UserCode, FirstName, LastName, Gender, YearOfBirth, AboutMe, Long, Lat FROM Users Where NeighborhoodName='" + neiName + "' AND (FirstName LIKE '%" + firstName + "%' OR LastName LIKE '%" + firstName + "%') AND (FirstName LIKE '%" + lastName + "%' OR LastName LIKE '%" + lastName + "%') ;";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    User user = new User();
                    user.UserId = Convert.ToInt32(dr["UserCode"]);
                    user.FirstName = (string)dr["FirstName"];
                    user.LastName = (string)dr["LastName"];
                    user.Gender = Convert.ToInt32(dr["Gender"]);
                    user.YearOfBirth = Convert.ToInt32(dr["YearOfBirth"]);
                    if (dr["AboutMe"].GetType() != typeof(DBNull))
                    {
                        user.AboutMe = (string)dr["AboutMe"];
                    }
                    user.Lan = Convert.ToDouble(dr["Long"]);
                    user.Lat = Convert.ToDouble(dr["Lat"]);



                    userByNameList.Add(user);
                }

                return userByNameList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }


        //filter neighboors by search name
        public List<User> GetAllUsersByName(string neiName, string userName)
        {
            List<User> userByNameList = new List<User>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT UserCode, FirstName, LastName, Gender, YearOfBirth, AboutMe, Long, Lat FROM Users Where NeighborhoodName='" + neiName + "' AND (FirstName LIKE '%" + userName + "%' OR LastName LIKE '%" + userName + "%');";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    User user = new User();
                    user.UserId = Convert.ToInt32(dr["UserCode"]);
                    user.FirstName = (string)dr["FirstName"];
                    user.LastName = (string)dr["LastName"];
                    user.Gender = Convert.ToInt32(dr["Gender"]);
                    user.YearOfBirth = Convert.ToInt32(dr["YearOfBirth"]);
                    if (dr["AboutMe"].GetType() != typeof(DBNull))
                    {
                        user.AboutMe = (string)dr["AboutMe"];
                    }
                    user.Lan = Convert.ToDouble(dr["Long"]);
                    user.Lat = Convert.ToDouble(dr["Lat"]);


                    userByNameList.Add(user);
                }

                return userByNameList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        //get all neighboors with extraReg
        public List<User> getAllUsersInNei(string neiName, int userId)
        {
            List<User> userInNeiList = new List<User>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Users LEFT JOIN ON UsersAndIntrests.UserCode = Users.UserCode Where NeighborhoodName=";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    User user = new User();
                    user.UserId = Convert.ToInt32(dr["UserCode"]);
                    user.FirstName = (string)dr["FirstName"];
                    user.LastName = (string)dr["LastName"];
                    user.Gender = Convert.ToInt32(dr["Gender"]);
                    if (dr["AboutMe"].GetType() != typeof(DBNull))
                    {
                        user.AboutMe = (string)dr["AboutMe"];
                    }
                    user.Lat = Convert.ToDouble(dr["Lat"]);
                    user.Lan = Convert.ToDouble(dr["long"]);

                    userInNeiList.Add(user);
                }

                return userInNeiList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        //call match element
        public List<User> GetUsersMatch(int userId)
        {
            List<User> usersMatchList = new List<User>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                SqlCommand cmd = new SqlCommand("SP_calculateMatch", con);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@userCode", userId));
                // execute the command
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    User user = new User();
                    user.UserId = Convert.ToInt32(dr["UserCode"]);
                    user.FirstName = (string)dr["FirstName"];
                    user.LastName = (string)dr["LastName"];
                    user.Gender = Convert.ToInt32(dr["Gender"]);
                    user.YearOfBirth = Convert.ToInt32(dr["YearOfBirth"]);
                    if (dr["AboutMe"].GetType() != typeof(DBNull))
                    {
                        user.AboutMe = (string)dr["AboutMe"];
                    }
                    user.Lan = Convert.ToDouble(dr["Long"]);
                    user.Lat = Convert.ToDouble(dr["Lat"]);
                    user.MatchRate = Math.Round(Convert.ToDouble(dr["FinalScore"]) * 100, 1);


                    usersMatchList.Add(user);
                }

                return usersMatchList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        //filter neighboors by search intrest type
        public List<User> GetAllUsersByIntrest(string neiName, int userIntrest)
        {
            List<User> userByNameList = new List<User>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT Users.UserCode, FirstName, LastName, Gender, YearOfBirth, AboutMe, Lat,Long FROM Users LEFT JOIN UsersAndIntrests  ON Users.UserCode = UsersAndIntrests.UserCode Where NeighborhoodName='" + neiName + "' AND IntrestId=" + userIntrest;

                SqlCommand cmd = new SqlCommand(selectSTR, con);


                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    User user = new User();
                    user.UserId = Convert.ToInt32(dr["UserCode"]);
                    user.FirstName = (string)dr["FirstName"];
                    user.LastName = (string)dr["LastName"];
                    user.Gender = Convert.ToInt32(dr["Gender"]);
                    user.YearOfBirth = Convert.ToInt32(dr["YearOfBirth"]);
                    if (dr["AboutMe"].GetType() != typeof(DBNull))
                    {
                        user.AboutMe = (string)dr["AboutMe"];
                    }
                    user.Lat = Convert.ToDouble(dr["Lat"]);
                    user.Lan = Convert.ToDouble(dr["Long"]);

                    userByNameList.Add(user);
                }

                return userByNameList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        //**********************JOB-TITLE************************

        //get all jt
        public List<JobTitle> GetAllJT()
        {
            List<JobTitle> allJT = new List<JobTitle>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * FROM JobTitle";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    JobTitle JT = new JobTitle();
                    JT.JobCode = Convert.ToInt32(dr["Code"]);
                    JT.JobName = (string)dr["JobName"];

                    allJT.Add(JT);
                }

                return allJT;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //**********************Intrests************************

        //get sub intrests by mainInerest
        public List<Intrests> GetSubIntrests(string mainI)
        {
            List<Intrests> allIntrests = new List<Intrests>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select Id, SubCat from Interests where MainCat='" + mainI + "';";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Intrests interest = new Intrests();
                    interest.Id = Convert.ToInt32(dr["Id"]);
                    interest.Subintrest = (string)dr["SubCat"];

                    allIntrests.Add(interest);
                }

                return allIntrests;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //get all intrests from db
        public List<Intrests> GetAllIntrests()
        {
            List<Intrests> allIntrests = new List<Intrests>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select distinct MainCat, SubCat, Id, Icon from Interests where Icon IS NOT NULL";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Intrests interest = new Intrests();
                    interest.Id = Convert.ToInt32(dr["Id"]);
                    interest.MainInterest = (string)dr["MainCat"];
                    interest.Subintrest = (string)dr["SubCat"];
                    interest.Icon = (string)dr["Icon"];

                    allIntrests.Add(interest);
                }

                return allIntrests;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }


        //************************address***********************

        //Get a list of the neighboorhoods in the city
        public List<Neighborhood> getAllNeiInCity(string cityName)
        {
            List<Neighborhood> neiList = new List<Neighborhood>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT NeiID, NeiName FROM Neighborhoods where CityName='" + cityName + "';";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Neighborhood nei = new Neighborhood();
                    nei.NCode = (string)dr["NeiID"];
                    nei.Name = (string)dr["NeiName"];

                    neiList.Add(nei);
                }

                return neiList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public List<City> getAllCities()
        {
            List<City> cityList = new List<City>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT CityCode,CityName FROM City";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    City city = new City();
                    city.CityCode = Convert.ToInt32(dr["CityCode"]);
                    city.CityName = (string)dr["CityName"];

                    cityList.Add(city);
                }

                return cityList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //********************************Param***********************************


        public List<Param> getAllParams()
        {
            List<Param> allParams = new List<Param>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select ParamId, NameHeb  from Params";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Param param = new Param();
                    param.ParamCode = Convert.ToInt32(dr["ParamId"]);
                    param.ParamNameHeb = (string)dr["NameHeb"];

                    allParams.Add(param);
                }

                return allParams;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }




        //*****************Votes**************************


        //add new vote
        public int addNewVoteToDB(int categoryId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = "UPDATE Params SET Votes= Votes+1 WHERE ParamId =" + categoryId;      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        public int UpdateParamsValue()
        {
            SqlConnection con;
            con = connect("DBConnectionString"); // create the connection
            SqlCommand cmd = new SqlCommand("SP_calculateParamsUser", con);

            try
            {


                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                // cmd.Parameters.Add(new SqlParameter("@userCode", userId));
                // execute the command
                // SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
    }
}