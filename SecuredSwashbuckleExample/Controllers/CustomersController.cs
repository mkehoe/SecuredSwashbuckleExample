using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using SecuredSwashbuckleExample.Models;

namespace SecuredSwashbuckleExample.Controllers
{
    [RoutePrefix("api/Customers")]
    public class CustomersController : ApiController
    {
        Customer[] customers = new Customer[]
        {
            new Customer { Id = 1, Name = "Mike1 Test", PhoneNumber = "2225559090" },
            new Customer { Id = 2, Name = "Mike2 Test", PhoneNumber = "2225559091" },
            new Customer { Id = 3, Name = "Mike3 Test", PhoneNumber = "2225559092" }

        };

        [Authorize]
        [Route("")]
        public IEnumerable<Customer> GetAllProducts()
        {
            return customers;
        }

        [Authorize]
        [Route("")]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = customers.FirstOrDefault((c) => c.Id == id);
            if(customer == null)
            {
                return NotFound();
            }
            return Ok(customer);

        }


    }
}
