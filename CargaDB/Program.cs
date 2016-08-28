using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CargaDB
{
    public class Program
    {
        public static bool stop = true;

        private Object thisLock = new Object();

        public static void Main(string[] args)
        {
            Program pro = new Program();

            for (int i = 0; i < 100; i++)
            {
                Thread t = new Thread(dataCall);
                t.Name = i.ToString();
                t.Start();
                Console.WriteLine("Starting {0}", i);
            }

            Console.ReadLine();
            stop = false;
        }

        public static void dataCall()
        {
            var rnd = new Random();
            var name = Thread.CurrentThread.Name;
            Console.WriteLine("entering Thread {0}", name);
            while (stop)
            {
                AdventureWorksEntities db = new AdventureWorksEntities();
                db.Database.CommandTimeout = 600;
                int ticks = rnd.Next(0, 3000);
                var x = db.SalesOrderHeaderEnlarged.Where(xso => xso.PurchaseOrderNumber.Contains(ticks.ToString()));
                var y = x.ToList();
                if (x.Any())
                {
                    x.FirstOrDefault().ModifiedDate = DateTime.Today;
                    db.SaveChanges();
                }
                Thread.Sleep(0);
                Console.WriteLine("{3}  count {0}, {1},  {2}", y.Count(), y.Any() ? y.FirstOrDefault().PurchaseOrderNumber : "", stop, name);
            }
            Console.WriteLine("Finsih");
        }
    }
}
