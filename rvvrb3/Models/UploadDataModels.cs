using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rvvrb3.Models
{
    public class UploadDataModel
    {
        public UploadDataModel()
        {
            songname = "blank";
            songtracknum = "blank";
            songartist = "blank";
            songdescription = "blank";
        }

        public string songname { get; set; }
        public string songtracknum { get; set; }
        public string songartist { get; set; }
        public string songdescription { get; set; }
    }
}