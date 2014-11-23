using CrazyMelsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrazyMelsWeb.Controllers
{
    public class SearchController : ApiController
    {
        private CrazyMelsEntities db = new CrazyMelsEntities();

        public IEnumerable<CrazyMelDataModel> Get()
        {
            List<CrazyMelDataModel> data = new List<CrazyMelDataModel>();

            //TODO: add some sort of request query
            /*
            IQueryable<Cart> carts = from cart in db.C_Cart
                                     //where
                                     select new Cart
                                     {
                                         orderID = cart.orderID,
                                         prodID = cart.prodID,
                                         quantity = cart.quantity
                                     };

            IQueryable<Product> products = from product in db.C_Product
                                     //where
                                     select new Product(product);

            IQueryable<Order> orders = from order in db.C_Order
                                     //where
                                     select new Order(order);

            IQueryable<Customer> customers = from customer in db.C_Customer
                                     //where
                                     select new Customer(customer);
             * */

            /*
            data.AddRange(carts.ToArray());s
            data.AddRange(products.ToArray());
            data.AddRange(orders.ToArray());
            data.AddRange(customers.ToArray());
             * */

            data.AddRange( db.C_Cart
                .Where(c => true)
                .ToList()
                .Select(c => new Cart { orderID = c.orderID, prodID = c.prodID, quantity = c.quantity })
            );

            data.AddRange(db.C_Product
                .Where(p => true)
                .ToList()
                .Select(p => new Product { prodID = p.prodID, price = p.price, prodName = p.prodName, prodWeight = p.prodWeight, inStock = p.inStock})
            );

            data.AddRange(db.C_Order
                .Where(o => true)
                .ToList()
                .Select(o => new Order { orderID = o.orderID, orderDate = o.orderDate, custID = o.custID, poNumber = o.poNumber })
            );

            data.AddRange(db.C_Customer
                .Where(c => true)
                .ToList()
                .Select(c => new Customer { custID = c.custID, firstName = c.firstName, lastName = c.lastName, phoneNumber = c.phoneNumber })
            );

            /*
            data.AddRange(db.C_Cart.Where(c => true).ToList());
            data.AddRange(db.C_Customer.Where(c => true).ToList());
            data.AddRange(db.C_Order.Where(c => true).ToList());
            data.AddRange(db.C_Product.Where(c => true).ToList());
             */

            return data.ToArray();

        }
    }
}
