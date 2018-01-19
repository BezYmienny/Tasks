using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            Task<int> t = Task.Run(() =>
            {
                //source.Cancel();
                //token.
                source.Cancel();
                token.ThrowIfCancellationRequested();
                

                return 42;
                //source.Cancel();
            },token);
            
            
            t.ContinueWith((i,j) =>
            {
                Console.WriteLine("@Canceled !");
                
            },TaskContinuationOptions.OnlyOnCanceled,token);

            t.ContinueWith((i,j) =>
           {
               Console.WriteLine("Faulted");
           }, TaskContinuationOptions.OnlyOnFaulted,token);
            
           
           
                var completedTask = t.ContinueWith((i, j) =>
               {
                   try
                   {
                       Console.WriteLine("Completed");
                   }
                   catch
                   {
                   }
               }, TaskContinuationOptions.OnlyOnRanToCompletion, token);

            Console.WriteLine("processing ....");
            Console.ReadKey();
            try
            {
                completedTask.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (Exception e in ae.InnerExceptions)
                {
                    if (e is TaskCanceledException)
                        Console.WriteLine("Unable to compute mean: {0}",
                                          ((TaskCanceledException)e).Message);
                    else
                        Console.WriteLine("Exception: " + e.GetType().Name);
                }
            }
            finally { }
            {
                source.Dispose();
            }
            Console.WriteLine("hokus pokus");
            Console.ReadKey();
        }
    }
}
