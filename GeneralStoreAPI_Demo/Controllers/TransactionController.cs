using GeneralStoreAPI_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GeneralStoreAPI_Demo.Controllers
{
    public class TransactionController : ApiController
    {
        private StoreDbContext _context = new StoreDbContext();

        //Post
        public IHttpActionResult Post(Transaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            if (ModelState.IsValid)
            {
                Customer customer = _context.Customers.Find(transaction.CustomerId);
                if (customer == null)
                {
                    return BadRequest("Customer not found.");
                }
                Product product = _context.Products.Find(transaction.ProductSKU);
                if (product == null)
                {
                    return BadRequest("Product not found.");
                }
                if (transaction.ItemCount > (product.NumberInInventory + 1))
                {
                    return BadRequest($"Not Enough { product.Name} items in stock. \n" +
                                $"Please enter a quantity less than {product.NumberInInventory + 1}");
                }
                _context.Transactions.Add(transaction);
                product.NumberInInventory -= transaction.ItemCount;
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }


        //Get
        public IHttpActionResult Get()
        {
            List<Transaction> transactions = _context.Transactions.ToList();
            if (transactions.Count != 0)
            {
                return Ok(transactions);
            }
            return BadRequest("Your database contains no Transactions... DO SOME WORK");
        }



        //Get{id}

        public IHttpActionResult Get(int id)
        {

            Transaction transaction = _context.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);

        }

        //Put{id}

        public IHttpActionResult Put([FromUri]int id, [FromBody]Transaction newTransaction)
        {
            if (ModelState.IsValid)
            {
                Transaction transaction = _context.Transactions.Find(id);
                Customer customer = _context.Customers.Find(newTransaction.CustomerId);
                Product product = _context.Products.Find(newTransaction.ProductSKU);
                if (transaction != null)
                {
                    if (customer == null)
                    {
                        return BadRequest("Customer not found");
                    }
                    if (product == null)
                    {
                        return BadRequest("Product not found");
                    }
                    transaction.CustomerId = newTransaction.CustomerId;
                    transaction.ProductSKU = newTransaction.ProductSKU;
                    if (newTransaction.ItemCount > product.NumberInInventory)
                    {
                        return BadRequest($"Not Enough {product.Name} items in stock. \n" +
                            $"Please enter a quantity less than {product.NumberInInventory + 1}");
                    }
                    transaction.ItemCount = newTransaction.ItemCount;
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            return InternalServerError();
        }
        //Delete{id}
        public IHttpActionResult Delete(int id)
        {
            Transaction transaction = _context.Transactions.Find(id);

            if (transaction == null)
            {
                return NotFound();
            }
            _context.Transactions.Remove(transaction);
            if (_context.SaveChanges() == 1)
            {
                return Ok("Transaction Deleted");
            }
            return InternalServerError();
        }
    }
}
