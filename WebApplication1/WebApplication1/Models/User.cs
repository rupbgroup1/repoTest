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
        int imageId;
        Image image;
        string imagePath;
        Address address;
        bool isPrivateName;
        int jobTitleId;
        string workPlace;
        string familyStatus;
        int numOfChildren;
        string aboutMe;
        Kids[] kids;
        Intrests[] intrests;
        int addressId;
        JobTitle jobTitle;


        public User(string email, string password, string firstName, string lastName, int gender, int yearOfBirth, bool isPrivateName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            YearOfBirth = yearOfBirth;
            IsPrivateName = isPrivateName;
        }

        public User()
        {

        }

        public User(string email, string password)
        {
           Email = email;
           Password = password;
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

        public int UserId { get => userId; set => userId = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int Gender { get => gender; set => gender = value; }
        public int YearOfBirth { get => yearOfBirth; set => yearOfBirth = value; }
        public int ImageId { get => imageId; set => imageId = value; }
        public Address Address { get => address; set => address = value; }
        public bool IsPrivateName { get => isPrivateName; set => isPrivateName = value; }
        public int JobTitleId { get => jobTitleId; set => jobTitleId = value; }
        public string WorkPlace { get => workPlace; set => workPlace = value; }
        public string FamilyStatus { get => familyStatus; set => familyStatus = value; }
        public int NumOfChildren { get => numOfChildren; set => numOfChildren = value; }
        public string AboutMe { get => aboutMe; set => aboutMe = value; }
        public Kids[] Kids { get => kids; set => kids = value; }
        public Intrests[] Intrests { get => intrests; set => intrests = value; }
        public Image Image { get => image; set => image = value; }
        public int AddressId { get => addressId; set => addressId = value; }
        public JobTitle JobTitle { get => jobTitle; set => jobTitle = value; }
        public string ImagePath { get => imagePath; set => imagePath = value; }
    }
}