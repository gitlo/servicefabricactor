using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace Basket.ClientCaller
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                var timer = new Stopwatch();                
                try
                {
                    timer.Start();

                    for (int counter = 1; counter < 1000; counter++)
                    {
                        var basket = new Model.Basket
                        {
                            Id = counter
                        };
                        basket.BasketItems.Add(new Model.BasketItem { Name = "item" });

                        var json = JsonConvert.SerializeObject(basket);
                        var content = new StringContent(json);
                        content.Headers.ContentType.MediaType = "application/json";

                        var response = await client.PostAsync("http://kesdev2:8326/api/Basket", content);
                        Console.WriteLine($"Response {response.StatusCode.ToString()}");
                    }

                    timer.Stop();
                    Console.WriteLine($"Timer {timer.ElapsedMilliseconds / 1000}");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);                    
                }               
            }
            Console.ReadKey();
        }
    }
}
