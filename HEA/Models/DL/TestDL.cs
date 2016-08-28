using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HEA.Models.DTO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hangfire;

namespace HEA.Models.DL
{
    public class TestDL : EnviromentDL
    {


        public IEnumerable<SalesOrderHeaderEnlarged>
        GetBy(Expression<Func<SalesOrderHeaderEnlarged, bool>> predicate)
        {
            return db.SalesOrderHeaderEnlarged.Where(predicate);
        }

        public SalesOrderHeaderEnlarged
        GetById(int id)
        {
            var result = db.SalesOrderHeaderEnlarged.Where(s => s.SalesOrderID == id);
            if (result.Any())
                return db.SalesOrderHeaderEnlarged.Where(s => s.SalesOrderID == id).FirstOrDefault();
            else
                return null;
        }

        public bool Update(int salesID)
        {

            BackgroundJob.Enqueue(() => _Update(salesID));
            return true;
        }

        public void _Update(int salesID)
        {
            var result = db.SalesOrderHeaderEnlarged.Where(s => s.SalesOrderID == salesID);

            result.FirstOrDefault().ModifiedDate = DateTime.Now;

            db.SaveChanges();
        }
    }
}