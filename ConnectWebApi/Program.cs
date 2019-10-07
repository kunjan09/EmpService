using EmployeeDataAccess;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConnectWebApi
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var yourusername = "male";
            var yourpwd = "male";


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44386/api/");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                         $"{yourusername}:{yourpwd}"
                        )));


            var responseTask = client.GetAsync("Emp");
            responseTask.Wait();

            if (responseTask.Result.IsSuccessStatusCode)
            {
                var returnedEmp = responseTask.Result.Content.ReadAsAsync<Employee[]>();
                returnedEmp.Wait();

                foreach (Employee e in returnedEmp.Result)
                {
                    Console.WriteLine(e.FirstName);
                }




            }



        }

        static void ShowEmployee(Employee e)
        {
            Console.WriteLine($"Name: {e.FirstName}\t" + $"{e.LastName}\tGender: {e.Gender}");
        }


        //static async Task<Employee> GetEmployeeAsync(string path)
        //{
        //    Employee emp = null;



        //}
    }
}
