using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientWindowsForms
{
    public class Client<T> where T : class
    {
        public HttpClient client = new HttpClient();

        public Client()
        {
            this.client.BaseAddress = new Uri("http://localhost:53098/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync(string path)
        {
            T var = null;
            
            HttpResponseMessage response = await this.client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var = await response.Content.ReadAsAsync<T>();
            }
            return var;
        }
            
    }
}
