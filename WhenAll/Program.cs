using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhenAll
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            Console.WriteLine("Gonna take 3 seconds");

            p.DoSomethingAsync().Wait();

            Console.WriteLine("Done");
            Console.WriteLine("Start fetching URLs");
            var urls = p.DownloadUrls().Result;
            foreach (var url in urls)
            {
                Console.WriteLine(url);
            }
        }

        async Task DoSomethingAsync()
        {
            var task1 = Task.Delay(TimeSpan.FromSeconds(1));
            var task2 = Task.Delay(TimeSpan.FromSeconds(2));
            var task3 = Task.Delay(TimeSpan.FromSeconds(3));

            await Task.WhenAll(task1, task2, task3);
        }

        async Task<IEnumerable<string>> DownloadUrls()
        {
            var task1 = Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith<string>((t) => "http://www.google.com");
            var task2 = Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith<string>((t) => "http://www.bing.com");

            return await Task.WhenAll(task1, task2);
        }
    }
}
