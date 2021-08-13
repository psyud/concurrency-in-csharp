using System;
using System.Threading.Tasks;

namespace ValueTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var p = new Program();
            p.ConsumeAsync().GetAwaiter().GetResult();
        }

        ValueTask<int> MethodAsync()
        {
            return new ValueTask<int>(99);
        }
        async Task ConsumeAsync()
        {
            var valueTask = MethodAsync();
            Console.WriteLine(await valueTask.AsTask());
            Console.WriteLine(await valueTask.AsTask());
        }
    }
}
