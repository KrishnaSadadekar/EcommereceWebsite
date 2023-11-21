using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ashion.Models
{
    public class Item
    {
        public int ProId { get; set; }
        public int CatId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Nullable<int> Unit { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }

        public Item()
        {
        }


    }
}