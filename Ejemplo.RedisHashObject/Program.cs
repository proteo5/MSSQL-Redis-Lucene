
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejemplo.RedisHashObject
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            var redis = new RedisClient("localhost", 6379, db: 6);
            var myO = redis.As<MyObject>();

            var storedObject = new MyObject();
            storedObject.Id = redis.IncrementValue("UniqueUserId");
            storedObject.Name = "Test Object";
            storedObject.StreetAddress = "Test Avenue 2";
            storedObject.ZipCode = "00100";

            // Guardar Objeto
            myO.Store(storedObject);

            // Fetch object
            var datos = myO.GetAll();
            var dato = datos.FirstOrDefault();


        }


    }

    public class MyObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
    }

}
