using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //PostUser(Console.ReadLine());
            //GetUser(Convert.ToInt32(Console.ReadLine());

            Console.Read();
        }

        static void PostUser(string nick)
        {
            USER user = new USER() { Nick = nick };

            HttpClient clint = new HttpClient();
            clint.BaseAddress = new Uri("http://localhost:53098/");
            var myContent = JsonConvert.SerializeObject(user);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = clint.PostAsync("/api/USERs/", byteContent).Result;
        }

        static async void GetUser(int id)
        {

            //USER user = new USER() { Nick = nick };

            HttpClient clint = new HttpClient();
            clint.BaseAddress = new Uri("http://localhost:53098/");
            HttpResponseMessage response = clint.GetAsync("/api/USERs/").Result;
            //var emp = await response.Content.ReadAsAsync<USER>();

            Console.WriteLine(response.Content);

        }
        static async Task<USER> GetProductAsync(string path)
        {
            USER user = null;

            HttpClient clint = new HttpClient();
            clint.BaseAddress = new Uri("http://localhost:53098/");

            HttpResponseMessage response = await clint.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                //user = await response.Content.ReadAsAsync<USER>();
            }
            return user;


            /*
             * https://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client
             */
        }
    }
}
