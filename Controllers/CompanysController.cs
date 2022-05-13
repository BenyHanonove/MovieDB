using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MovieDB.Models;


namespace MovieDB.Controllers
{
    public class CompanysController : ApiController
    {
        public IEnumerable<Company> Get()
        {           
            Company c = new Company();
            List<Company> cList = c.GetCompany();
            return cList ;
        }



        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<controller>
        public int Post([FromBody] Company company)
        {
            
            return company.InsertCompany();
             

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