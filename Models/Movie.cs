using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieDB.Models.DAL;

namespace MovieDB.Models
{
    public class Movie
    {
        int id;
        string title;
        string description;
        ArrayList genre;
        DateTime releaseDate;
        string img;
        double score;

        public Movie()
        {

        }

        public Movie(int id,string title,string description, ArrayList genre,DateTime releaseDate, string img,double score)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.genre = genre;
            this.releaseDate = releaseDate;
            this.img = img; 
            this.score = score; 
        }

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public ArrayList Genre { get => genre; set => genre = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public string Img { get => img; set => img = value; }
        public double Score { get => score; set => score = value; }

        public string genreToString()
        {
            string str = "";
            for(int i = 0; i < genre.Count; i++)
            {
                str +=genre[i].ToString()+", ";
            }
            return str;
        }

        public int InsertMovie(string id)
        {
            DataServices ds = new DataServices();
            ds.InsertMovie(this);
            ds.addMovieById(this, id);
            return 1;

        }



        public List<Movie> Get()
        {
            DataServices ds2 = new DataServices();
            List<Movie> mList = ds2.Get();
            return mList;
        }

        //public List<Movie> Get(String Title)
        //{
        //    DataServices ds = new DataServices();
        //    List<Movie> mList = ds.Get(Title);
        //    return mList;
        //}
        public Movie Get(String Title,int Id)
        {
            DataServices ds = new DataServices();
            Movie newMovie = ds.GetMovie(Title,Id);
            return newMovie;
        }

        public List<Movie> getMovies(string comapnyId)
        {
            DataServices ds2 = new DataServices();
            List<Movie> mList = ds2.GetCompanyMoviesById(comapnyId);
            return mList;
        }


        public static implicit operator List<object>(Movie v)
        {
            throw new NotImplementedException();
        }
    }
}