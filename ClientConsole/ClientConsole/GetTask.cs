using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class GetTask<T> where T : class
    {
        public HttpClient client = new HttpClient();

        public GetTask()
        {
            client.BaseAddress = new Uri("http://localhost:53098/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        //GET
        public async Task<T> GetUserAsync(string path)
        {
            T var = null;
            HttpResponseMessage response = await this.client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var = await response.Content.ReadAsAsync<T>();
            }
            return var;
        }

        ////GET ALL
        //async Task<T> GetUsersAsync(string path)
        //{
        //    T var = null;
        //    HttpResponseMessage response = await this.client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var = await response.Content.ReadAsAsync<T>();
        //    }
        //    return var;
        //}
    }
}