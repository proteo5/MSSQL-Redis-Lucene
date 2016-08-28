using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HEA.Models.DL;
using HEA.Models.DTO;
using HEA.Models.HL;

namespace HEA.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Update(int SalesID)
        {
            TestDL testDL = new TestDL();
            testDL.Update(SalesID);
            return Json(new { regreso = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TraeSales(string PO)
        {
            var cache = RedisConnectorHelper.Connection.GetDatabase(10);
            int i = 0;

            bool exitLoop = false;

            List<object> result = new List<object>();
            while (!exitLoop)
            {
                string SalesOrderIDs = cache.HashGet($"SalesPO_{PO}:{i}", "SalesOrderID");
                if (SalesOrderIDs == null)
                {
                    exitLoop = true;
                }
                else
                {
                    int SalesOrderID = int.Parse(SalesOrderIDs);
                    string PurchaseOrderNumber = cache.HashGet($"SalesPO_{PO}:{i}", "PurchaseOrderNumber");
                    string SalesOrderNumber = cache.HashGet($"SalesPO_{PO}:{i}", "SalesOrderNumber");
                    string DateMod = cache.HashGet($"SalesPO_{PO}:{i}", "DateMod");
                    result.Add(new { SalesOrderID, PurchaseOrderNumber, SalesOrderNumber, DateMod });
                    i++;
                }
            }

            if (i != 0)
            {
                return Json(new { regreso = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TestDL testDL = new TestDL();
                var regreso = testDL.GetBy(s => s.PurchaseOrderNumber == PO)
                    .Select(s => new
                    {
                        s.SalesOrderID,
                        s.PurchaseOrderNumber,
                        s.SalesOrderNumber,
                        DateMod = s.ModifiedDate.ToString("dd/MM/yyyy")
                    });

                foreach (var item in regreso)
                {
                    cache.HashSet($"SalesPO_{PO}:{i}", "SalesOrderID", item.SalesOrderID.ToString());
                    cache.HashSet($"SalesPO_{PO}:{i}", "PurchaseOrderNumber", item.PurchaseOrderNumber);
                    cache.HashSet($"SalesPO_{PO}:{i}", "SalesOrderNumber", item.SalesOrderNumber);
                    cache.HashSet($"SalesPO_{PO}:{i}", "DateMod", item.DateMod);
                    i++;
                }
                return Json(new { regreso = regreso }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}