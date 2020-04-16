﻿using System;
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
                    k.Id= Convert.ToInt32(dr["UserCode"]);
                    k.YearOfBirth= Convert.ToInt32(dr["YearOfBirth"]);
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
                String selectSTR = "select* from UsersAndIntrests left join Interests on UsersAndIntrests.UserCode = Interests.Id  where UserCode =" + u.UserId;
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
                        JobTitle JT = new JobTitle(code,name);
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


        //*****************Neighboors***************************
        
        //filter neighboors by search *full* name
        public List<User> GetAllUsersByfullName(string neiName, string firstName,string lastName)
        {
            List<User> userByNameList = new List<User>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT UserCode, FirstName, LastName, Gender, AboutMe, Long, Lat FROM Users Where NeighborhoodName='" + neiName + "' AND (FirstName LIKE '%" + firstName + "%' OR LastName LIKE '%" + firstName + "%') AND (FirstName LIKE '%" + lastName + "%' OR LastName LIKE '%" + lastName + "%') ;";
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

                String selectSTR = "SELECT UserCode, FirstName, LastName, Gender, AboutMe, Long, Lat FROM Users Where NeighborhoodName='" + neiName + "' AND (FirstName LIKE '%" + userName + "%' OR LastName LIKE '%" + userName + "%');";
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
        //need to fix!!
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
        //filter neighboors by search intrest type
        public List<User> GetAllUsersByIntrest(string neiName, int userIntrest)
        {
            List<User> userByNameList = new List<User>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT Users.UserCode, FirstName, LastName, Gender, AboutMe, Lat,Long FROM Users LEFT JOIN UsersAndIntrests  ON Users.UserCode = UsersAndIntrests.UserCode Where NeighborhoodName='" + neiName + "' AND IntrestId="+ userIntrest;
                
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
                    interest.Icon= (string)dr["Icon"];

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

                String selectSTR = "SELECT NeiID, NeiName FROM Neighborhoods where CityName='"+cityName+"';";
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

        //public List<City> getAllCities()
        //{
        //    List<City> cityList = new List<City>();
        //    SqlConnection con = null;

        //    try
        //    {
        //        con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

        //        String selectSTR = "SELECT * FROM City";
        //        SqlCommand cmd = new SqlCommand(selectSTR, con);

        //        // get a reader
        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

        //        while (dr.Read())
        //        {   // Read till the end of the data into a row
        //            City city = new City();
        //            city.CityCode = Convert.ToInt32(dr["CityCode"]);
        //            city.CityName = (string)dr["CityName"];
        //            city.Size = Convert.ToInt32(dr["Size"]);

        //            cityList.Add(city);
        //        }

        //        return cityList;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }

        //    }
        //}

        //public List<Address> getAllStreets(City city)
        //{
        //    List<Address> streetsList = new List<Address>();
        //    SqlConnection con = null;
        //    int cityCode = city.CityCode;

        //    try
        //    {
        //        con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

        //        String selectSTR = "SELECT * FROM Street where CityCode=" + cityCode;
        //        SqlCommand cmd = new SqlCommand(selectSTR, con);

        //        // get a reader
        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

        //        while (dr.Read())
        //        {   // Read till the end of the data into a row
        //            Address street = new Address();
        //            street.StreetCode = Convert.ToInt32(dr["StreetCode"]);
        //            street.StreetName = (string)dr["StreetName"];
        //            street.StreetNum = Convert.ToInt32(dr["StreetCode"]);
        //            streetsList.Add(street);
        //        }

        //        return streetsList;
        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }

        //    }

        //}

        //********************************
    }
}