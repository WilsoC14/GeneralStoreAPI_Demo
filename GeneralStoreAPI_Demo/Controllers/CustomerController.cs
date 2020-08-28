using GeneralStoreAPI_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GeneralStoreAPI_Demo.Controllers
{
    public class CustomerController : ApiController
    {
        private StoreDbContext _context = new StoreDbContext();

        //Post --Create
        public IHttpActionResult Post(Customer customer)
        {
            //if an empty Customer is passed in
            if (customer == null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            //if the ModelState is not Valid
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //Get -- Read All

        public IHttpActionResult Get()
        {
            List<Customer> customers = _context.Customers.ToList();
            if (customers.Count != 0)
            {
                return Ok(customers);
            }
            return BadRequest("Your database contains no Customers");
        }
        //Get{id} -- Read By ID
        public IHttpActionResult Get(int id)
        {

            Customer customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);

        }



        //Put{id} -- Update By ID
        public IHttpActionResult Put([FromUri]int id, [FromBody]Customer newCustomer)
        {
            if (ModelState.IsValid)
            {
                Customer customer = _context.Customers.Find(id);
                if (customer != null)
                {
                    customer.FirstName = newCustomer.FirstName;
                    customer.LastName = newCustomer.LastName;
                    _context.SaveChanges();
                    return Ok("Customer Updated");
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        //Delete{id} -- Delete By ID
        public IHttpActionResult Delete(int id)
        {
            Customer customer = _context.Customers.Find(id);
                if(customer == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            if(_context.SaveChanges() == 1)
            {
                return Ok("Customer Deleted");
            }
            return InternalServerError();
        }
    }
}
