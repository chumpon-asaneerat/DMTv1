using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace OwinSelfHostServerSample
{
    public class ProductController
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
        }


        public class ProductsController : ApiController
        {
            Product[] products = new Product[]
            {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M },
            new Product { Id = 4, Name = "Driver", Category = "Hardware", Price = 6.12M }
            };

            public IEnumerable<Product> Get()
            {
                return products;
            }

            public Product Get(int id)
            {
                var product = products.FirstOrDefault((p) => p.Id == id);
                if (product == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return product;
            }

            public IEnumerable<Product> Get(string category)
            {
                return products.Where(p => string.Equals(p.Category, category,
                        StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
