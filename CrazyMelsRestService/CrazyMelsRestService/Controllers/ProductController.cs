using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CrazyMelsRestService;

namespace CrazyMelsRestService.Controllers
{
    public class ProductController : ApiController
    {
        private CrazyMelsRestServiceEntities db = new CrazyMelsRestServiceEntities();

        // GET api/Product
        public IQueryable<C_Product> GetC_Product()
        {
            return db.C_Product;
        }

        // GET api/Product/5
        [ResponseType(typeof(C_Product))]
        public IHttpActionResult GetC_Product(int id)
        {
            C_Product c_product = db.C_Product.Find(id);
            if (c_product == null)
            {
                return NotFound();
            }

            return Ok(c_product);
        }

        // PUT api/Product/5
        public IHttpActionResult PutC_Product(int id, C_Product c_product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != c_product.prodID)
            {
                return BadRequest();
            }

            db.Entry(c_product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!C_ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Product
        [ResponseType(typeof(C_Product))]
        public IHttpActionResult PostC_Product(C_Product c_product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.C_Product.Add(c_product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = c_product.prodID }, c_product);
        }

        // DELETE api/Product/5
        [ResponseType(typeof(C_Product))]
        public IHttpActionResult DeleteC_Product(int id)
        {
            C_Product c_product = db.C_Product.Find(id);
            if (c_product == null)
            {
                return NotFound();
            }

            db.C_Product.Remove(c_product);
            db.SaveChanges();

            return Ok(c_product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool C_ProductExists(int id)
        {
            return db.C_Product.Count(e => e.prodID == id) > 0;
        }
    }
}