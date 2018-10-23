using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace learnThread
{
    //class Program
    //{
    ////////////////////////
    // #1 pratice
    ////////////////////////
    //static void Main(string[] args)
    //{
    //    Thread t1 = new Thread(MyBackgroundTask);
    //    t1.Start();

    //    for (int i = 0; i < 500; i++)
    //    {
    //        Console.Write(".");
    //    }
    //    Console.ReadLine();
    //}

    //static void MyBackgroundTask()
    //{
    //    for (int i = 0; i < 500; i++)
    //    {
    //        Console.Write("[" + Thread.CurrentThread.ManagedThreadId + "]");
    //    }
    //}

    ////////////////////////
    // #2 pratice, input parameter
    ////////////////////////
    //static void Main()
    //{
    //    Thread t1 = new Thread(MyBackgroundTask);
    //    Thread t2 = new Thread(MyBackgroundTask);
    //    Thread t3 = new Thread(MyBackgroundTask);

    //    t1.Start('X');
    //    t2.Start('Y');
    //    t3.Start('Z');

    //    for (int i = 0; i < 500; i++)
    //    {
    //        Console.Write(".");
    //    }
    //    Console.ReadKey();
    //}

    //static void MyBackgroundTask(object param)
    //{
    //    for (int i = 0; i < 500; i++)
    //    {
    //        Console.Write(param.ToString());
    //    }
    //}

    ////////////////////////
    // #3 pratice, wait and pause the Thread
    ////////////////////////
    //static void Main()
    //{
    //    Thread t1 = new Thread(MyBackgroundTask);
    //    Thread t2 = new Thread(MyBackgroundTask);
    //    Thread t3 = new Thread(MyBackgroundTask);


    //    t2.Start('Y');
    //    t3.Start('Z');

    //    t2.Join();
    //    t3.Join();
    //    Thread.Sleep(2000);

    //    for (int i = 0; i < 500; i++)
    //    {
    //        Console.Write(".");
    //    }

    //    t1.Start('X');
    //    t1.Join();

    //    Console.ReadKey();
    //}

    //static void MyBackgroundTask(object param)
    //{
    //    for (int i = 0; i < 500; i++)
    //    {
    //        Console.Write(param.ToString());
    //    }
    //}

    //    }

    ////////////////////////
    // #4 pratice, share variable between Threads
    ////////////////////////
    //class Program
    //{
    //    static void Main()
    //    {
    //        new SharedStateDemo().Run();
    //        Console.ReadLine();
    //    }
    //}

    //public class SharedStateDemo
    //{
    //    private int itemCount = 0; // the number of added to cart

    //    public void Run()
    //    {
    //        var t1 = new Thread(AddToCart);
    //        var t2 = new Thread(AddToCart);

    //        t1.Start(300);
    //        t2.Start(100);
    //    }

    //    private void AddToCart(object simulateDelay)
    //    {
    //        itemCount++;

    //        /*
    //         * 用 Thread.Sleep 來模擬這項工作所花的時間，時間長短
    //         * 由呼叫端傳入的 simulateDelay 參數指定，以便藉由改變
    //         * 此參數來觀察共享變數值的變化。
    //         */
    //        Thread.Sleep((int)simulateDelay);
    //        Console.WriteLine("Items in cart: {0}", itemCount);
    //    }
    //}


    ////////////////////////
    // #5 pratice, using Lock to avoid 
    ////////////////////////
    class Program
    {
        static void Main()
        {
            new SharedStateDemo().Run();
            Console.ReadLine();
        }
    }

    public class SharedStateDemo
    {
        private int itemCount = 0; 
        private object locker = new Object(); // 用於獨佔鎖定的物件

        public void Run()
        {
            var t1 = new Thread(AddToCart);
            var t2 = new Thread(AddToCart);

            t1.Start(300);
            t2.Start(100);
        }

        private void AddToCart(object simulateDelay)
        {
            Console.WriteLine("Enter thread {0}", Thread.CurrentThread.ManagedThreadId);
            lock (locker)
            {
                itemCount++;

                /*
                 * 用 Thread.Sleep 來模擬這項工作所花的時間，時間長短
                 * 由呼叫端傳入的 simulateDelay 參數指定，以便藉由改變
                 * 此參數來觀察共享變數值的變化。
                 */
                Thread.Sleep((int)simulateDelay);
                Console.WriteLine("Items in cart: {0} on thread {1}", itemCount,
                    Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}
