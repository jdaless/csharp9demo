using System;
using System.Threading.Tasks;

namespace csharp9demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new System.Net.Http.HttpClient
            {
                BaseAddress = new Uri("http://taco-randomizer.herokuapp.com")
            };
            // Get a good taco. All tacos are good.
            Console.WriteLine((await new ApiWrapper(httpClient).GetGoodRandomTaco((t, i) => true)).ToString());
        }
    }
}
