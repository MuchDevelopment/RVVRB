using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using rvvrb3.Models;
using System.Security.Claims;
using System.Security;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace rvvrb3.API
{
    //[Route("api/songs")]
    public class HomepageController : ApiController
    {
        MiddleTier mt = new MiddleTier();
        
        [Authorize]
        //[Route("api/songs/{start}/{limit}")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage HomeSongs([FromUri]int start,[FromUri]int limit)
        {
            int rowcount = start + limit;


            string json = JsonConvert.SerializeObject(mt.ReturnHomeSongs()); 
            StringContent sc = new StringContent(json);
            HttpResponseMessage rm = new HttpResponseMessage();

            rm.Content = sc;

            return rm;
        }

       // [Route("api/songs/{id}")]
       // [System.Web.Http.HttpGet]
       // public HttpResponseMessage GetSong(int id)
       // {
       //     int thisid = id;
       //     string json = JsonConvert.SerializeObject(mt.ReturnSong(thisid)); 
       //     StringContent sc = new StringContent(json);
       //     HttpResponseMessage rm = new HttpResponseMessage();
       //
       //     rm.Content = sc;
       //
       //     return rm;
       // }

    }
}
