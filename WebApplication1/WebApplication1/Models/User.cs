using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class User
    {
        int userId;
        string email;
        string password;
        string firstName;
        string lastName;
        int gender;
        int yearOfBirth;
        Address address;
        int jobTitleId;
        string workPlace;
        string familyStatus;
        int numOfChildren;
        string aboutMe;
        Kids[] kids;
        Intrests[] intrests;
        int addressId;
        JobTitle jobTitle;
        string imagePath;
        double lat;
        double lan;
        string cityName;
        string neighborhoodName;
        

        public User()
        {

        }

        public int addToDB(User newUser)
        {
            DBservices dbs = new DBservices();
            return dbs.addNewUserToDB(newUser);
        }

        public User getUserByDetails(User user)
        {
            DBservices dbs = new DBservices();
            return dbs.getUserByDetails(user);
        }

        public int GetUserByEmail(string userEmail)
        {
            DBservices dbs = new DBservices();
            return dbs.GetUserByEmail(userEmail);
        }

        public List<User> GetAllUsersByName(string cityName, string userName)
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllUsersByName(cityName, userName);
        }

        public List<User> GetAllUsersByfullName(string cityName, string firstName, string lastName)
        {
            DBservices dbs = new DBservices();
            
            return dbs.GetAllUsersByfullName(cityName, firstName, lastName);
        }

        public List<User> GetUsersByIntrest(string neiId, int intrest)
        {
            DBservices dbs = new DBservices();

            return dbs.GetAllUsersByIntrest(neiId, intrest);
        }

        public int updateUserExtraDetails(User u)
        {
            DBservices dbs = new DBservices();
            return dbs.updateUserExtraDetails(u);
        }

        public List<User> GetUsersMatch(int userId)
        {
            DBservices dbs = new DBservices();

            return dbs.GetUsersMatch(userId);
        }
        



        public int UserId { get => userId; set => userId = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int Gender { get => gender; set => gender = value; }
        public int YearOfBirth { get => yearOfBirth; set => yearOfBirth = value; }
        public Address Address { get => address; set => address = value; }
        public int JobTitleId { get => jobTitleId; set => jobTitleId = value; }
        public string WorkPlace { get => workPlace; set => workPlace = value; }
        public string FamilyStatus { get => familyStatus; set => familyStatus = value; }
        public int NumOfChildren { get => numOfChildren; set => numOfChildren = value; }
        public string AboutMe { get => aboutMe; set => aboutMe = value; }
        public Kids[] Kids { get => kids; set => kids = value; }
        public Intrests[] Intrests { get => intrests; set => intrests = value; }
        public int AddressId { get => addressId; set => addressId = value; }
        public JobTitle JobTitle { get => jobTitle; set => jobTitle = value; }
        public string ImagePath { get => imagePath; set => imagePath = value; }
        public double Lat { get => lat; set => lat = value; }
        public double Lan { get => lan; set => lan = value; }
        public string CityName { get => cityName; set => cityName = value; }
        public string NeighborhoodName { get => neighborhoodName; set => neighborhoodName = value; }
    }
}