using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ashion.Models
{
    public class InvoiceM
    {
        //Order Details
        public int invoiceId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }


        //Personal Details
        public string Name { get; set; }
        public string LastN { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }

        //-------------------
        public string SelectedPaymentMethod { get; set; }



    }
}