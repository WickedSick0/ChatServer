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
        [HttpPost,Route("api/Upload/")]
        public Task<HttpResponseMessage> PostFile()
        {
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/IMG");
            var provider = new MultipartFormDataStreamProvider(root);

            var task = request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(o =>
                {

                    string file1 = provider.FileData.First().LocalFileName;
                    FileInfo fileold = new FileInfo(file1);
                    File.Move(fileold.FullName, Path.Combine(fileold.DirectoryName, fileold.Name + ".jpg"));
                    fileold.Delete();
                    // this is the file name on the server where the file was saved 
                    
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
