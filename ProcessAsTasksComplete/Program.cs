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

            p.ProcessAsTasksCompleteTheWrongWay().GetAwaiter().GetResult();
            p.ProcessAsTasksCompleteTheRightWay().GetAwaiter().GetResult();
        }

        async Task ProcessAsTasksCompleteTheRightWay()
        {
            var task1 = DelayAndReturnAsync(3);
            var task2 = DelayAndReturnAsync(2);
            var task3 = DelayAndReturnAsync(1);

            var tasks = new[] { task1, task2, task3 };
            var processingTasks = tasks.Select(t => HandleAsync(t));

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
            await Task.Delay(x);
            return x;
        }
    }
}
