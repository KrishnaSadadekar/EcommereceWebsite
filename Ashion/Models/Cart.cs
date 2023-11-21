using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ashion.Models
{
    public class Cart
    {


        public int proId { get; set; }
        public string proName { get; set; }
        public string proImage { get; set; }
        public int qty { get; set; }
        public int price { get; set; }
        public int bill { get; set; }

        public string size { get; set; }

    }
}