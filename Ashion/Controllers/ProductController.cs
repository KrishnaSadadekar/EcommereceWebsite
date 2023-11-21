using Ashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ashion.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        //Listing Availbility Product listing

        
        AshionEntities5 db = new AshionEntities5();
        public ActionResult ProductDisplay()
        {
            List<ProductList> ls = new List<ProductList>();
            foreach (ProductList p in db.ProductLists) 
            {
                ProductList item = new ProductList() 
                { 
                   ProId= p.ProId,
                   Name=p.Name,
                   CatId=p.CatId,
                   Description=p.Description,
                   Image=p.Image,
                   Unit=p.Unit
                
                };
                ls.Add(item);
            }
            return View(ls);
        }

        

    }
}