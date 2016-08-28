using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejemplo.RedisHash
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            //Console.WriteLine("Saving random data in cache {0}", DateTime.Now);
            //program.SaveBigData();

            Console.WriteLine("Reading data from cache {0}", DateTime.Now);
            program.ReadData();

            //Console.WriteLine("Delete data from cache {0}", DateTime.Now);
            //program.DeleteData();

            Console.WriteLine("Finish {0}", DateTime.Now);
            Console.ReadLine();

        }

        public void ReadData()
        {
            var cache = RedisConnectorHelper.Connection.GetDatabase(4);
            var devicesCount = 10000;
            for (int i = 0; i < devicesCount; i++)
            {
                string value = "";
                for (int ii = 0; ii < 10; ii++)
                {
                    var val = cache.HashGet($"TabledData:{i}", $"Field:{ii}");
                    value += $"{val,6}";
                }
                //Console.WriteLine($"TabledData:{i}: Valor={value}");
            }
        }

        public void SaveBigData()
        {
            var devicesCount = 10000;
            var rnd = new Random();
            var cache = RedisConnectorHelper.Connection.GetDatabase(4);

            for (int i = 1; i < devicesCount; i++)
            {
                for (int ii = 0; ii < 10; ii++)
                {
                    var value = rnd.Next(0, 10000);
                    cache.HashSet($"TabledData:{i}", $"Field:{ii}", value);
                }
            }
        }

        public void DeleteData()
        {
            var cache = RedisConnectorHelper.Connection.GetDatabase(4);
            var devicesCount = 10000;
            for (int i = 0; i < devicesCount; i++)
            {
                for (int ii = 0; ii < 10; ii++)
                {
                    var value = cache.HashDelete($"TabledData:{i}", $"Field:{ii}");
                }
            }
        }
    }
}
