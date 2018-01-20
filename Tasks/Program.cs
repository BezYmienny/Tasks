using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<Int32[]> parent = Task.Run(() =>
            {
                var results = new Int32[3];

                new Task(() => results[0] = 0,TaskCreationOptions.AttachedToParent).Start();
                //Console.WriteLine(results[0]);

                new Task(() => results[1] = 1, TaskCreationOptions.AttachedToParent).Start();
                //Console.WriteLine(results[1]);
                new Task(() => results[2] = 2, TaskCreationOptions.AttachedToParent).Start();

                Console.WriteLine("Main Task");
                return results;
            });

           // Console.WriteLine(" results is {0} big",parent.Result);

            var finalTask = parent.ContinueWith(
                parentTask =>
            {
                foreach (int i in parent.Result)   ///Error in Chapter 1 page 13 -> there is parentTask - it isn't connected to parent ( task ) !!!
                    Console.WriteLine(i);
            });

            finalTask.Wait();
                    
            Console.WriteLine("hokus pokus");
            Console.ReadKey();
        }
    }
}
