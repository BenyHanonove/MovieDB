using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using MovieDB.Models;

namespace cDB.Models.DAL
{
    public class CompanyDataServices
    {
        public SqlDataAdapter da;
        public DataTable dt;
        static List<Company> companyList = new List<Company>();

        public int InsertCompany(Company c)
        {

   
            SqlConnection con = Connect();
            String cStr = BuildInsertCommand(c);

            // Create Command
            SqlCommand command = CreateCommand(cStr, con);

            // Execute
            int numAffected = command.ExecuteNonQuery();

            // Close Connection

            con.Close();

            return numAffected;
        }

        public List<Company> GetCompany()
        {

            SqlConnection con = Connect();

            string commandStr = "SELECT * FROM allCompanies_2022";

            SqlCommand command = new SqlCommand(commandStr, con);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);


            while (dr.Read())
            {

                string userName = dr["USERNAME"].ToString();
                string passWord = dr["PASSWORD"].ToString();
                string name = dr["NAME"].ToString();
                string country = dr["COUNTRY"].ToString();
                int estYear = Convert.ToInt32(dr["ESTYEAR"]);
                int theaterNum = Convert.ToInt32(dr["THEATERNUM"]);
                string logoImg = dr["LOGOIMG"].ToString();
                companyList.Add(new Company(userName, passWord, name, country, estYear,theaterNum, logoImg));

            }
           
            con.Close();


            return companyList;

        }

        private String BuildInsertCommand(Company c)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}','{3}', {4}, {5}, '{6}')", c.USERNAME, c.PASSWORD, c.NAME, c.COUNTRY, c.ESTYEAR, c.THEATERNUM, c.LOGOIMG);
            String prefix = "INSERT INTO allCompanies_2022 " + "([USERNAME], [PASSWORD], [NAME], [COUNTRY], [ESTYEAR], [THEATERNUM], [LOGOIMG]) ";
            command = prefix + sb.ToString();

            return command;
        }

        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }

        private SqlConnection Connect()
        {


            // read the connection string from the web.config file
            string connectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // create the connection to the db
            SqlConnection con = new SqlConnection(connectionString);

            // open the database connection
            con.Open();

            return con;

        }


        public List<Company> Get()
        {
            return companyList;
        }

        public CompanyDataServices()
        {

        }


    }

}