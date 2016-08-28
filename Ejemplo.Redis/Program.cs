using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejemplo.Redis
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            //Console.WriteLine("Saving random data in cache {0}", DateTime.Now);
            //program.SaveBigData();

            //Console.WriteLine("Reading data from cache {0}", DateTime.Now);
            //program.ReadData();

            Console.WriteLine("Delete data from cache {0}", DateTime.Now);
            program.DeleteData();

            Console.WriteLine("Finish {0}",DateTime.Now);
            Console.ReadLine();
                        
        }

        public void ReadData()
        {
            var cache = RedisConnectorHelper.Connection.GetDatabase();
            var devicesCount = 100000;
            for (int i = 0; i < devicesCount; i++)
            {
                var value = cache.StringGet($"Device_Status:{i}");
                //Console.WriteLine($"Valor={value}");
            }
        }

        public void SaveBigData()
        {
            var devicesCount = 100000;
            var rnd = new Random();
            var cache = RedisConnectorHelper.Connection.GetDatabase();

            for (int i = 1; i < devicesCount; i++)
            {
                var value = rnd.Next(0, 10000);
                cache.StringSet($"Device_Status:{i}", value);
            }
        }

        public void DeleteData()
        {
            var cache = RedisConnectorHelper.Connection.GetDatabase();
            var devicesCount = 100000;
            for (int i = 0; i < devicesCount; i++)
            {
                var value = cache.KeyDelete($"Device_Status:{i}");
            }
        }
    }
}
