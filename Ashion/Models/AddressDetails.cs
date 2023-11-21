using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ashion.Models
{
    public class AddressDetails
    {
        [Required]
        public string Name { get; set; }
        public string LastN { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

    }
}