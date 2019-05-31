using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestApiClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new RestApiClient.RestService("https://jsonplaceholder.typicode.com/");

           var result = api.GetData<dynamic>("todos/1");
            
           Console.WriteLine(JsonConvert.SerializeObject(result));
           Console.ReadKey();
        }
    }
}
