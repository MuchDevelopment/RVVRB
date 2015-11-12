using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using rvvrb3.Models;

namespace rvvrb3.Controllers
{
    public class FilesController : ApiController
    {
        [HttpPost] // This is from System.Web.Http, and not from System.Web.Mvc
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            // so this is how you can get the original file name
            string originalFileName = GetDeserializedFileName(result.FileData.First());

            // uploadedFileInfo object will give you some additional stuff like file length,
            // creation time, directory name, a few filesystem methods etc..
            //var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
            string localPath = result.FileData.First().LocalFileName;

            // Remove this line as well as GetFormData method if you're not
            // sending any form data with your upload request
            UploadDataModel fileUploadObj = (UploadDataModel)GetFormData<UploadDataModel>(result);


            //Move the file to the path
            List<string> returnData = MoveFile(fileUploadObj, localPath, originalFileName);
            
            //sync with the db
            SyncWithDb(fileUploadObj,returnData);

            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            return this.Request.CreateResponse(HttpStatusCode.OK, new { fileUploadObj });
        }

        private List<string> MoveFile(UploadDataModel fileUploadObj,string localPath, string originalFileName)
        {
            RVVRBDBEntities2 rv = new RVVRBDBEntities2();
            ApplicationDbContext db = new ApplicationDbContext();
            
            int userID = Convert.ToInt32(User.Identity.GetUserId());
            string oguid = userID.ToString();

            var user = rv.AspNetUsers.Single(i => i.Id == userID);

            string extension = Path.GetExtension(originalFileName); 

            string musicPath = "/Files/" + user.Id + "/";
            string filename = user.Id + "-" + DateTime.Now.Ticks;
            string fullfilepath = musicPath + filename + extension;
            string serverpath = HttpContext.Current.Server.MapPath("~") + "\\Files\\" + user.Id + "\\";
            string fullserverpath = serverpath + filename + extension;
            
            //check to see if user folder exists and creates if null
            bool pathExists = Directory.Exists(serverpath);
            if (!pathExists)
                Directory.CreateDirectory(serverpath);
            

            //now move the file from temp to the user path
            File.Move(localPath, fullserverpath);

            //pack up relevant data for db call
            List<string> data = new List<string>();
            data.Add(userID.ToString());
            data.Add(fullfilepath);

            return data;

        }

        private void SyncWithDb(UploadDataModel fileUploadObj,List<string> fromMove)
        {
            MiddleTier mt = new MiddleTier();

            //file attributes
            string n = fileUploadObj.songname;
            string a = fileUploadObj.songartist;
            int t;
            int.TryParse(fileUploadObj.songtracknum,out t);
            if (t == int.MinValue || t<1) t = 0;
            //int t = Convert.ToInt32(fileUploadObj.songtracknum);
            string d = fileUploadObj.songdescription;

            //add song to db
            mt.CreateSong(Convert.ToInt32(fromMove[0]), n, t, d, a, fromMove[1]);
        }

        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            // IMPORTANT: replace "(tilde)" with the real tilde character
            // (our editor doesn't allow it, so I just wrote "(tilde)" instead)
            var uploadFolder = "~/App_Data/Tmp/FileUploads"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        // Extracts Request FormatData as a strongly typed model
        private object GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData
                    .GetValues(0).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData)) { }
                    return JsonConvert.DeserializeObject<T>(unescapedFormData);
            }

            return null;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
    }
}
