using System;
using System.Threading.Tasks;

namespace CompletedTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var myClass = new MyClass();
            var task = myClass.GetNumberAsync();
            Console.WriteLine(task.Result);
            myClass.DoSomethingAsync().GetAwaiter().GetResult();

            var exceptionTask = myClass.NotImplementedAsync();
            Console.WriteLine("Not exception yet");

            exceptionTask.GetAwaiter().GetResult();
        }
    }

    interface IMyInterface 
    {
        Task<int> GetNumberAsync();

        Task DoSomethingAsync();

        Task NotImplementedAsync();
    }

    class MyClass : IMyInterface
    {
        public Task DoSomethingAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<int> GetNumberAsync()
        {
            return await Task.FromResult(42);
        }

        public Task NotImplementedAsync()
        {
            return Task.FromException(new NotImplementedException());
        }
    }
}
