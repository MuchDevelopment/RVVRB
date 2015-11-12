using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using rvvrb3.Models;

namespace rvvrb3.API
{


    [Route("api/comments")]
    public class CommentsController : ApiController
    {
        //wire up the db
        MiddleTier mt = new MiddleTier();
        
        [Route("api/comments/{id}")]
        public HttpResponseMessage GetTrackComments(string id)
        {
            if (!String.IsNullOrEmpty(id) && id != "undefined")
            {
            int thisid = Convert.ToInt32(id);
            string json = JsonConvert.SerializeObject(mt.GetComments(thisid));
            StringContent sc = new StringContent(json);
            HttpResponseMessage rm = new HttpResponseMessage();

            rm.Content = sc;

            return rm;
            }

            return null;
        }

        [Route("api/comments/add")]
        [HttpPost]
        public async void PostComment()
        {
            //var provider = GetMultipartProvider();
            JObject posted = await Request.Content.ReadAsAsync<JObject>();
            CommentDataModel commentObj = posted.ToObject<CommentDataModel>();
            

            int userID = Convert.ToInt32(User.Identity.GetUserId());
            string comment = commentObj.comment;
            int trackID = Convert.ToInt32(commentObj.trackID);
            
            mt.CreateComment(userID,trackID,comment);
        }


        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var cacheFolder = "~/App_Data/Tmp/Comments"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(cacheFolder);
            return new MultipartFormDataStreamProvider(root);
        }

    }
}
