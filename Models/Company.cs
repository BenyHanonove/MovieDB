using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cDB.Models.DAL;
using MovieDB.Models.DAL;

namespace MovieDB.Models
{
    public class Company
    {
        string userName;
        string password;      
        string country;
        string name;
        int estYear;
        int theaterNum;
        string logoImg;


        public Company()
        {

        }

        public Company(string username, string password ,string name, string country ,int estYear,int theaterNum, string logoImg) 
        {
            this.userName = username;
            this.password = password;
            this.name = name;
            this.country = country;
            this.estYear = estYear; 
            this.theaterNum = theaterNum;
            this.logoImg = logoImg;

        }

        public  string USERNAME { get { return userName; } set { userName = value; } }
        public string PASSWORD { get { return password; } set { password = value; } }   
        public string NAME { get { return name; } set { name = value; } }   
        public string COUNTRY { get { return country; } set { country = value; } }  
        public int ESTYEAR { get { return estYear; } set { estYear = value; } } 
        public int THEATERNUM { get { return theaterNum; } set { theaterNum = value; } }    
        public string LOGOIMG { get { return logoImg; } set { logoImg = value; } }    
        

        public int InsertCompany()
        {
            CompanyDataServices ds = new CompanyDataServices();
             ds.InsertCompany(this);
            return 1;
        }

        public List<Company> GetCompany()
        {
            CompanyDataServices ds2 = new CompanyDataServices();
            List<Company> cList = ds2.GetCompany();
            return cList;

        }

    }
}