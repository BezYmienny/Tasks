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
            Task<int> t = Task.Run(() =>
           {
               for(int i=0;i<100;i++)
               {
                   Console.Write("*");
               }
               Console.WriteLine("!!!");
               return 42;
           });

            t.Wait();
            Console.WriteLine(t.Result);
            Console.ReadKey();
        }
    }
}
