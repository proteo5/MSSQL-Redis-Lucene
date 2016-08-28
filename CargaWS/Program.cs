using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CargaWS
{
    class Program
    {
        public static bool stop = true;

        private Object thisLock = new Object();

        public static void Main(string[] args)
        {
            Program pro = new Program();

            bool cual = true;

            for (int i = 0; i < 100; i++)
            {
                if (cual)
                {
                    Thread t = new Thread(dataCall);
                    t.Name = i.ToString();
                    t.Start();
                }
                else
                {
                    Thread t = new Thread(dataCall2);
                    t.Name = i.ToString();
                    t.Start();
                }
                cual = !cual;
                Console.WriteLine("Starting {0}", i);
            }

            Console.ReadLine();
            stop = false;
        }

        public static void dataCall()
        {
            var name = Thread.CurrentThread.Name;
            Console.WriteLine("entering Thread {0}", name);
            AdventureWorksEntities db = new AdventureWorksEntities();
            db.Database.CommandTimeout = 600;
            var result = db.SalesOrderHeaderEnlarged.Take(10000);
            foreach (var item in result)
            {
                var client = new WebClient();
                var content = client.DownloadString($"http://localhost:58372/test/TraeSales?PO={item.PurchaseOrderNumber}");
                Console.WriteLine("Se ejecuto un Select");
                Thread.Sleep(0);
            }
            Console.WriteLine("Finsih");
        }

        public static void dataCall2()
        {
            var name = Thread.CurrentThread.Name;
            Console.WriteLine("entering Thread {0}", name);
            AdventureWorksEntities db = new AdventureWorksEntities();
            db.Database.CommandTimeout = 600;
            var result = db.SalesOrderHeaderEnlarged.Take(10000);
            foreach (var item in result)
            {
                var client = new WebClient();
                var content = client.DownloadString($"http://localhost:58372/test/update?SalesID={item.SalesOrderID}");
                Console.WriteLine("Se ejecuto un update");
                Thread.Sleep(0);
            }

            Console.WriteLine("Finsih");
        }
    }
}
