using MovieDB.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace MovieDB.Controllers
{


    public class MoviesController : ApiController
    {
        //GET api/<controller>
        public IEnumerable<Movie> Get()
        {
            Movie m = new Movie();
            List<Movie> mmList = m.Get();
            return mmList;
        }

        public List<Movie> Get(string companyId)
        {
            Movie movie = new Movie();
            return movie.getMovies(companyId);
        }


        //[HttpGet]
        //[Route("api/Movies/{id}")]
        //public List<Movie> Get(string id)
        //{
        //    Movie m = new Movie();
        //    List<Movie> gmList = m.getMovies();
        //    return gmList;
        //}


        //GET api/<controller>/5
        public string Get(int id)
        {
            return id.ToString(); 
        }


    [HttpGet]
    [Route("api/Movies/{Title}/{Id}")]
    public Movie Get(string Title,int Id)
    {
        Movie newMovie = new Movie();
        Movie m = new Movie();
         newMovie = m.Get(Title,Id);
        return newMovie;
    }


    // POST api/<controller>
    public int Post([FromBody] Movie movie, string id)
        {
             movie.InsertMovie(id);
            return 1;

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            
        }



    }
}