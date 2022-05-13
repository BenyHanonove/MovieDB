using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Configuration;


namespace MovieDB.Models.DAL
{
    public class DataServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        static List<Movie> movieList = new List<Movie>();
        static List<Movie> moviesList = new List<Movie>();
        static Movie movie = new Movie();




        public int InsertMovie(Movie movie)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand command = new SqlCommand();


            try
            {
                con = Connect(); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(movie);
             command = CreateCommand(cStr, con); // helper method to build the insert string

            try
            {
                int numEffected = command.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    return 0;
                }
                else throw;
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



        //public List<Movie> GetMovies()
        //{

        //    SqlConnection con = Connect();

        //    string commandStr = "SELECT * FROM allMovies_2022";

        //    SqlCommand command = new SqlCommand(commandStr, con);          

        //    SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

        //    while (dr.Read())
        //    {
        //        ArrayList genre = new ArrayList();

        //        int id = Convert.ToInt32(dr["Id"]);
        //        string title = dr["Title"].ToString();
        //        string description = dr["Description"].ToString();
        //         genre.Add(dr["Genre"].ToString());
        //        DateTime releaseDate = Convert.ToDateTime(dr["releaseDate"]);
        //        string img = dr["img"].ToString();
        //        double score = Convert.ToDouble(dr["score"]);
        //        moviesList.Add(new Movie(id,title,description,genre,releaseDate,img,score));

        //    }

        //    con.Close();

        //    return moviesList;

        //}

        public Movie GetMovie(string Title, int Id)
        {

            SqlConnection con = Connect();

            string commandStr = "SELECT * FROM allMovies_2022";

            SqlCommand command = new SqlCommand(commandStr, con);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                ArrayList genre = new ArrayList();

                int id = Convert.ToInt32(dr["Id"]);
                string title = dr["Title"].ToString();
                string description = dr["Description"].ToString();
                genre.Add(dr["Genre"].ToString());
                DateTime releaseDate = Convert.ToDateTime(dr["releaseDate"]);
                string img = dr["img"].ToString();
                double score = Convert.ToDouble(dr["score"]);
                moviesList.Add(new Movie(id, title, description, genre, releaseDate, img, score));

            }
            
            con.Close();

            foreach (Movie m in moviesList)
            {
                if (m.Title == Title && m.Id == Id)
                    movie = m;
            }
            return movie;

        }

        public int addMovieById(Movie movie, string id)
        {

            SqlConnection con = Connect();

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}')", id, movie.Id);
            String prefix = "INSERT INTO CompanyMovies_2022 " + "([companyId],[movieId])";

            string cStr = prefix + sb.ToString();

            SqlCommand command = CreateCommand(cStr, con);

            // Execute
            int numAffected = command.ExecuteNonQuery();

            con.Close();

            return numAffected;

        }
        public List<Movie> GetCompanyMoviesById(string comapnyId)
        {
            SqlConnection con = new SqlConnection();

            try
            {
                 con = Connect(); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT *"
                                    + " FROM CompanyMovies_2022 as c"
                                    + " INNER JOIN allMovies_2022 as m"
                                    + " ON c.movieId = m.id"
                                    + " WHERE c.companyId = " + "'" + comapnyId + "';";
                SqlCommand cmd = new SqlCommand(selectSTR, con);


                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                List<Movie> listOfMovies = new List<Movie>();
                while (dr.Read())
                {   // Read till the end of the data into a row
                    ArrayList genre = new ArrayList();

                    int id = Convert.ToInt32(dr["id"]);
                    string title = dr["title"].ToString();
                    string description = dr["description"].ToString();
                    genre.Add(dr["genre"].ToString());
                    DateTime releaseDate = Convert.ToDateTime(dr["releaseDate"]);
                    string img = dr["img"].ToString();
                    double score = Convert.ToDouble(dr["score"]);
                    Movie temp = (new Movie(id, title, description, genre, releaseDate, img, score));
                    listOfMovies.Add(temp);
                }
                return listOfMovies;

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


        private String BuildInsertCommand(Movie movie)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}','{2}','{3}', '{4}','{5}',{6})", movie.Id, movie.Title, movie.Description, movie.genreToString(), movie.ReleaseDate,  movie.Img, movie.Score);
            String prefix = "INSERT INTO allMovies_2022 " + "([id], [title], [description], [genre], [releaseDate], [img], [score]) ";
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

        public List<Movie> Get()
        {

            return movieList;
        }


        public DataServices()
        {

        }
    }
}
    
