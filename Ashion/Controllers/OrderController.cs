using Ashion.DbOperation;
using Ashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ashion.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order Manage Order and process from checkout to Payment and Confirmation

        Shopping pro = new Shopping();
        AshionEntities5 db = new AshionEntities5();
        public ActionResult Index()
        {
            Invoice d= Session["invoice"] as Invoice;

            return View(d);
        }

        [Authorize]
        public ActionResult CheckOut()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(InvoiceM model)
        {
            int objId = 2;
            List<Cart> li2 = Session["cart"] as List<Cart>;
            int billT = 0;
            foreach (var itemAmt in li2) { billT += itemAmt.bill; }

            if (ModelState.IsValid)
            {

                //Address Save
               // string add = pro.AddSave(model,AddId);

          //Invoice-------------------------------
            int d1 = pro.InvoiceCreate(model, billT,objId);

           //Order -------------------------------------
             int order = pro.placeOrder(li2, objId);
             if (order > 0)
               {

                    ViewBag.msg = "Order Done!";
                    Session.Remove("cart");
                    Session.Remove("total");
               }
             else
                {
                    ViewBag.msg = "Order is not Placed!";
                }
                Session["invoice"] = db.Invoices.FirstOrDefault(x => x.InvoiceId == d1);

            }

                return RedirectToAction("BillDetails");
        }



        public ActionResult BillDetails ()
        {
            Invoice d = Session["invoice"] as Invoice;
            BillDetail b = db.BillDetails.FirstOrDefault(x => x.InvoiceId == d.InvoiceId);

            return View(b);
        }


    }
}