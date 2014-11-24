using CrazyMelsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace CrazyMelsWeb.Controllers
{
    public class SearchController : ApiController
    {
        private CrazyMelsEntities db = new CrazyMelsEntities();

        private static Expression<Func<TItem, bool>> PropertyEquals<TItem, TValue>(
    PropertyInfo property, TValue value)
        {
            var param = Expression.Parameter(typeof(TItem));
            var body = Expression.Equal(Expression.Call(Expression.Property(param, property), typeof(object).GetMethod("ToString")),
                Expression.Constant(value));
            return Expression.Lambda<Func<TItem, bool>>(body, param);
        }

        [Route("api/search/{*sParameters}")]
        public IEnumerable<CrazyMelDataModel> Get(string sParameters)
        {
            List<CrazyMelDataModel> data = new List<CrazyMelDataModel>();

            if (!String.IsNullOrWhiteSpace(sParameters))
            {

                List<Customer> customers = new List<Customer>();
                List<Cart> carts = new List<Cart>();
                List<Order> orders = new List<Order>();
                List<Product> products = new List<Product>();

                IList<string> parameters = sParameters.Split('/');
                

                foreach (string param in parameters)
                {
                    IEnumerable<string> kvp = param.Split('=');

                    if (!string.IsNullOrWhiteSpace(kvp.First()) && !string.IsNullOrWhiteSpace(kvp.Last()))
                    {
                        IEnumerable<string> propertyLocatorPieces = kvp.First().Split('.');
                        if (propertyLocatorPieces.Count() == 2)
                        {
                            string className = propertyLocatorPieces.First();
                            string propertyName = propertyLocatorPieces.Last();
                            PropertyInfo propertyInfo = null;
                            string value = kvp.Last();

                            switch (className)
                            {
                                case "Customer":
                                    propertyInfo = typeof(C_Customer).GetProperty(propertyName);
                                    customers.AddRange(db.C_Customer.Where(PropertyEquals<C_Customer, string>(propertyInfo, value)).Select(c => new Customer { custID = c.custID, firstName = c.firstName, lastName = c.lastName, phoneNumber = c.phoneNumber }));
                                    break;
                                case "Cart":
                                    propertyInfo = typeof(C_Cart).GetProperty(propertyName);
                                    carts.AddRange(db.C_Cart.Where(PropertyEquals<C_Cart, string>(propertyInfo, value)).Select(c => new Cart { orderID = c.orderID, prodID = c.prodID, quantity = c.quantity }));
                                    break;
                                case "Order":
                                    propertyInfo = typeof(C_Order).GetProperty(propertyName);
                                    orders.AddRange(db.C_Order.Where(PropertyEquals<C_Order, string>(propertyInfo, value)).Select(c => new Order { custID = c.custID, orderDate = c.orderDate, orderID = c.orderID, poNumber = c.poNumber }));
                                    break;
                                case "Product":
                                    propertyInfo = typeof(C_Product).GetProperty(propertyName);
                                    products.AddRange(db.C_Product.Where(PropertyEquals<C_Product, string>(propertyInfo, value)).Select(c => new Product { prodID = c.prodID, prodName = c.prodName, prodWeight = c.prodWeight, price = c.price, inStock = c.inStock }));
                                    break;
                            }
                        }


                    }
                }

                data.AddRange(customers);
                data.AddRange(carts);
                data.AddRange(orders);
                data.AddRange(products);
            }

            

            

            return data.ToArray();

        }

       
    }
}
