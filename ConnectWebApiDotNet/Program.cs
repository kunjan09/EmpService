using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConnectWebApiDotNet
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44386/api/");




            var responseTask = client.GetAsync("Emp");
            responseTask.Wait();

            if (responseTask.Result.IsSuccessStatusCode)
            {
                var returnedEmp = responseTask.Result.Content.re
            }

        }
    }
}
