using System;
using System.Threading.Tasks;

namespace TaskThrowsException
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.TestAsync().GetAwaiter().GetResult();
        }

        async Task ThrowExceptionAsync()
        {
            // returns immediately when method is "awaited"
            await Task.Delay(TimeSpan.FromSeconds(1));
            throw new InvalidOperationException("Test");
        }

        async Task TestAsync()
        {
            var task = ThrowExceptionAsync();
            // at this point the "throw" line is not executed yet
            Console.WriteLine("Still alive");
            try
            {
                await task;
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine("Exception only when task is done and method throws");
            }
        }
    }
}
