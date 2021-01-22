using csharp9demo;
using System;

var httpClient = new System.Net.Http.HttpClient
{
    BaseAddress = new Uri("http://taco-randomizer.herokuapp.com")
};

// Get a good taco. All tacos are good.
Console.WriteLine((await new ApiWrapper(httpClient).GetGoodRandomTaco((_, _) => true)).ToString());
