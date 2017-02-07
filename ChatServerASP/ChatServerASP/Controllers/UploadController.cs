using ChatServerASP.Models;
using ChatServerASP.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ChatServerASP.Controllers
{
    public class UploadController : ApiController
    {
        private UserRepository Urep = new UserRepository();
        private User_tokensRepository Utrep = new User_tokensRepository();

        [HttpPost,Route("api/Upload/")]
        public Task<HttpResponseMessage> PostFile()
        {

            var cr = HttpContext.Current;
            string token = cr.Request.Form["token"];

            int id;
            if (!int.TryParse(cr.Request.Form["id"], out id))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            id = int.Parse(cr.Request.Form["id"]);

            if (Utrep.CheckToken(token, id) == false)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }



            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = System.Web.HttpContext.Current.Server.MapPath("~/Content/Photos");
            var provider = new MultipartFormDataStreamProvider(root);

            string userfilepath;
            USER u1 = Urep.FindById(id);
            var task = request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(o =>
                {

                    string file1 = provider.FileData.First().LocalFileName;
                    FileInfo fileold = new FileInfo(file1);
                    userfilepath = fileold.Name + "-" + u1.Login + ".jpg";
                    File.Move(fileold.FullName, Path.Combine(fileold.DirectoryName, userfilepath));
                    fileold.Delete();

                    u1.Photo = "/Content/Photos/" + userfilepath;
                    Urep.UpdateUser(u1);
                    
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("File uploaded.")
                    };
                }
            );
            return task;
        } 
    }
}
