using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rvvrb3.Models
{
    public class Song
    {
        public int ID { get; set; }
        public int userID { get; set; }
        public string songname { get; set; }
        public string songtracknum { get; set; }
        public string songartist { get; set; }
        public string songdescription { get; set; }
        public string url { get; set; }
        public DateTime time { get; set; }


    }
}