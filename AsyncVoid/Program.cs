using Nito.AsyncEx;
using System;

namespace AsyncVoid
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AsyncContext.Run(() => MainAsync(args));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }
        // BAD CODE!!!
        // In the real world, do not use async void unless you have to.
        static async void MainAsync(string[] args)
        {
            // Looks like you the exception is not caught...
            throw new NotImplementedException();
        }
    }
}
