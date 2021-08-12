using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessAsTasksComplete
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var p = new Program();

            Console.WriteLine("Wrong way:");
            p.ProcessAsTasksCompleteTheWrongWay().GetAwaiter().GetResult();
            Console.WriteLine("Right way:");
            p.ProcessAsTasksCompleteTheRightWay().GetAwaiter().GetResult();
        }

        async Task ProcessAsTasksCompleteTheRightWay()
        {
            var task1 = DelayAndReturnAsync(3);
            var task2 = DelayAndReturnAsync(2);
            var task3 = DelayAndReturnAsync(1);

            var tasks = new[] { task1, task2, task3 };
            var processingTasks = tasks.Select(t => HandleAsync(t));

            // This waits for all tasks to complete then collects the results
            // So task3 completes first, 1 is collected, then 2 is collected, then 3 is collected, and so on
            await Task.WhenAll(processingTasks.ToArray());
        }

        private async Task<int> HandleAsync(Task<int> t)
        {
            var result = await t;
            Console.WriteLine(result);

            return result;
        }

        async Task ProcessAsTasksCompleteTheWrongWay()
        {
            var task1 = DelayAndReturnAsync(3);
            var task2 = DelayAndReturnAsync(2);
            var task3 = DelayAndReturnAsync(1);

            foreach(var task in new[] { task1, task2, task3 })
            {
                // Because the loop is so fast, each task is scheduled at nearly the same time.
                // So even though task1 is scheduled first, it will return last.
                var result = await task;
                Console.WriteLine(result);
            }
        }

        async Task<int> DelayAndReturnAsync(int x)
        {
            await Task.Delay(TimeSpan.FromSeconds(x));
            return x;
        }
    }
}
