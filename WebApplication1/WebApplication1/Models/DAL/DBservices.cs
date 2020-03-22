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

        //*****************User***************************************
        
        //add new user
        private String BuildInsertCommand(User u)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}','{4}','{5}', '{6}' )", u.Email, u.Password, u.FirstName, u.LastName, u.Gender, u.YearOfBirth, u.IsPrivateName);
            String prefix = "INSERT INTO Users" + "(Email, PasswordUser, FirstName, LastName, Gender, YearOfBirth, IsPrivateName)";
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

        //get user by username and password
        public User getUserByDetails(User u)
        {
            User userDetails = new User();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                String selectSTR = "SELECT * FROM Users where Email='" + u.Email + "' AND PasswordUser='" + u.Password + "'";
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
                    userDetails.AddressId = Convert.ToInt32(dr["StreetCode"]);
                    userDetails.ImageId = Convert.ToInt32(dr["ImageId"]);
                    userDetails.IsPrivateName = Convert.ToBoolean(dr["IsPrivateName"]);
                    userDetails.JobTitleId = Convert.ToInt32(dr["JobTitleCode"]);
                    userDetails.WorkPlace = (string)dr["WorkPlace"];
                    userDetails.FamilyStatus = (string)dr["FamilyStatus"];
                    userDetails.NumOfChildren= Convert.ToInt32(dr["NumberOfChildren"]);
                    userDetails.AboutMe = (string)dr["AboutMe"];
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

        //get user by username and password
        public int getUserByDetails(string username, string password)
        {
            int userId = 0;
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                String selectSTR = "SELECT UserCode FROM Users where Email='" + username + "' AND PasswordUser='" + password + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                   userId = Convert.ToInt32(dr["UserCode"]);
                }

                return userId;
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

        //get all details by Id
        public User getUserDetails(int Id)
        {
            User userDetails = new User();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                String selectSTR = "SELECT * FROM Users where UserCode="+Id;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   
                   userDetails.FirstName = (string)dr["FirstName"];
                    userDetails.LastName = (string)dr["LastName"];
                    userDetails.Gender = Convert.ToInt32(dr["Gender"]);
                    userDetails.YearOfBirth = Convert.ToInt32(dr["YearOfBirth"]);
                    userDetails.AddressId = Convert.ToInt32(dr["StreetCode"]);
                    userDetails.ImageId = Convert.ToInt32(dr["ImageId"]);
                    userDetails.IsPrivateName = Convert.ToBoolean(dr["IsPrivateName"]);
                    userDetails.JobTitleId = Convert.ToInt32(dr["JobTitleCode"]);
                    userDetails.WorkPlace = (string)dr["WorkPlace"];
                    userDetails.FamilyStatus = (string)dr["FamilyStatus"];

                    
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

        //************************address***********************

        public List<City> getAllCities()
        {
            List<City> cityList = new List<City>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM City";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    City city = new City();
                    city.CityCode = Convert.ToInt32(dr["CityCode"]);
                    city.CityName = (string)dr["CityName"];
                    city.Size = Convert.ToInt32(dr["Size"]);

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

        public List<Address> getAllStreets(City city)
        {
            List<Address> streetsList = new List<Address>();
            SqlConnection con = null;
            int cityCode = city.CityCode;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Street where CityCode=" + cityCode;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Address street = new Address();
                    street.StreetCode = Convert.ToInt32(dr["StreetCode"]);
                    street.StreetName = (string)dr["StreetName"];
                    street.StreetNum = Convert.ToInt32(dr["StreetCode"]);
                    streetsList.Add(street);
                }

                return streetsList;
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

    }
}