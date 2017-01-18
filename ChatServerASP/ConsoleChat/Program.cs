using ChatServerASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConsoleChat
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            FindUser(1);

            Console.ReadLine();
        }

        static void FindUser(int id)
        {
            var resp = client.GetAsync(string.Format("api/USERs/{0}", id)).Result;
            resp.EnsureSuccessStatusCode();

            USER user = resp.Content.ReadAsAsync<ChatServerASP.Controllers.USERsController>().Result.GetUSER(id);
            Console.WriteLine("ID {0}: {1}" + user.GetUSER(id));
        }
    }
}
