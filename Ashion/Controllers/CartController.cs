using Ashion.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ashion.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart: Handling Shopping cart fucntionality ,Including adding and removing 


        List<Cart> li = new List<Cart>();


        AshionEntities5 db = new AshionEntities5();



        public ActionResult ProductDetails(int id)
        {
            ProductList query = db.ProductLists.Where(x => x.ProId == id).FirstOrDefault();
            Item item = new Item()
            {
                ProId = query.ProId,
                Name = query.Name,
                Unit = query.Unit,
                Image = query.Image,
                Description = query.Description


            };


            return View(item);
        }



        [HttpPost]
        public ActionResult ProductDetails(int id,string size,int qty)
        {
            ProductList p = db.ProductLists.Where(m => m.ProId == id).FirstOrDefault();
            Cart c = new Cart();
            c.proId = p.ProId;
            c.proName = p.Name;
            c.proImage = p.Image;
            c.price = Convert.ToInt32(p.Unit);
            c.qty = Convert.ToInt32(qty);
            c.bill = c.price * c.qty;
            c.size = size.ToString();
            if (Session["cart"] == null)
            {
                li.Add(c);
                Session["cart"] = li;
                Session["msg"] = "Product Added";
                return RedirectToAction("ProductDisplay","Product");
            }
            else
            {

                List<Cart> li2 = Session["cart"] as List<Cart>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.proId == c.proId)
                    {
                        item.qty += c.qty;
                        item.bill += c.bill;
                        flag = 1;

                    }

                }
                if (flag == 0)
                {

                    li2.Add(c);
                }

                Session["cart"] = li2;
                return RedirectToAction("ProductDisplay", "Product");
            }

        }


        public ActionResult MyCart()
        {
            List<Cart> li2 = Session["cart"] as List<Cart>;
            if (Session["cart"] != null)
            {

                int x = 0;
                foreach (var item in li2)
                {

                    x += item.bill;

                }
                Session["total"] = x;
                Session["totalC"] = li2.Count;

            }

            return View();
        }

        public ActionResult RemoveCart(int id)
        {
            if (Session["cart"] == null)
            {
                Session.Remove("total");
                Session.Remove("cart");
            }
            else
            {
                List<Cart> li2 = Session["cart"] as List<Cart>;
                Cart c = li2.Where(m => m.proId == id).FirstOrDefault();
                li2.Remove(c);
                int s = 0;
                foreach (var item in li2)
                {
                    s += item.bill;

                }
                Session["total"] = s;

            }
            return RedirectToAction("MyCart");
        }




        public ActionResult Index()
        {
            return View();
        }



    }
}