using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace rvvrb3.Services
{
    public class HttpCommunication : ApiController
    {
        //private string token;

        public string GetResponse(WebRequest request,string token)
        {
            string result = "";

            try
            {
                if (token != String.Empty) request.Headers.Add("Authorization", "Bearer " + token);
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";
                WebResponse resp = request.GetResponse();

                using (System.IO.Stream stream = resp.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        result = sr.ReadToEnd();                   
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return result;
        }
    }
}