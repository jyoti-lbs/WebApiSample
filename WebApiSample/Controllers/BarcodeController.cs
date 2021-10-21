using WebApiSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Diagnostics;
using WebApiSample.DataBase;

namespace WebApiSample.Controllers
{
    public class BarcodeController : ApiController
    {
        //Barcode[] barcodes = new Barcode[] 
        //{ 
        //    new Barcode { Id = 1, UPI = "11111" },
        //    new Barcode { Id = 2, UPI = "22222" },
        //    new Barcode { Id = 3, UPI = "33333" }
        //};

        public IEnumerable<Barcode> GetAllBarcodes()
        {
            using (var db = new BondCloudDbEntities())
            {
                return db.Barcodes.ToArray();
            }
        }

        public IHttpActionResult GetBarcodeById(int id)
        {
            using (var db = new BondCloudDbEntities())
            {
                var query = from p in db.Barcodes
                            where p.Id == id
                            select p;

                if (query.Count() == 0)
                {
                    return NotFound();
                }

                return Ok(query.ToArray());
            }
        }

        // POST api/persons
        public void Post([FromBody]Barcode value)
        {
            Debug.WriteLine(value);

            using (var db = new BondCloudDbEntities())
            {
                db.Barcodes.Add(value);
                db.SaveChanges();
            }
        }

        // PUT api/persons/5
        public void Put(int id, [FromBody] Barcode newValue)
        {
            using (var db = new BondCloudDbEntities())
            {
                Barcode value = (from p in db.Barcodes
                                 where p.Id == id
                               select p).FirstOrDefault();

                if (value == null)
                {
                    // TODO: Resturn an error.
                    return;
                }

                value.Barcode1 = newValue.Barcode1;

                db.SaveChanges();
            }
        }

        // DELETE api/persons/5
        public void Delete(int id)
        {
            using (var db = new BondCloudDbEntities())
            {
                Barcode value = (from p in db.Barcodes
                                 where p.Id == id
                                select p).FirstOrDefault();

                if (value == null)
                {
                    // TODO: Resturn an error.
                    return;
                }

                db.Barcodes.Remove(value);
                db.SaveChanges();
            }
        }


    }
}
