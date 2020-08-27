using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI_Demo.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}