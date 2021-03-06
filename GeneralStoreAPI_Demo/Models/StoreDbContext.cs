﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI_Demo.Models
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext() : base("DefaultConnection")
        {
            //asdfdsdf dont do this at home
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}