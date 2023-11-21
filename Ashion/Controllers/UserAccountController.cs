using Ashion.DbOperation;
using Ashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ashion.Controllers
{
    
    public class UserAccountController : Controller
    {
        AshionEntities5 db = new AshionEntities5();
        Account userCheck = new Account();
        int turnover = 0;
        // GET: UserAccount: Handling user and registeration, login and profile Management
        public ActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserSignUp model)
        {
            if (ModelState.IsValid)
            {
                int iss = userCheck.AddUser(model);

                if (iss > 0)
                {
                    ViewBag.msg = "Registeration Done!";
                }
                else
                {
                    ViewBag.msg = "Failed to Register!";

                }


            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(UserD t)
        {

            var query = db.UserDs.SingleOrDefault(m => m.EmailId==t.EmailId && m.Position == t.Position);
            if (query != null)
            {
                if (query.RoleType == "1")
                {
                    FormsAuthentication.SetAuthCookie(query.UserName, false);
                    Session["user"] = query.UserName;
                    Session["uid"] = query.UserId;
                    return RedirectToAction("ProductDisplay", "Product");
                }
                else if (query.RoleType == "2")
                {
                    FormsAuthentication.SetAuthCookie(query.UserName, false);
                    Session["user"] = query.UserName;
                    Session["uid"] = query.UserId;
                    return RedirectToAction("AdminDashboard", "UserAccount");

                }

            }
            else
            {
                TempData["msg"] = "Invalid UserName or Password";

            }

            return View();
        }




        [HttpPost]
        public JsonResult IsAlreadySigned(string Email)
        {

            bool isUnique = !db.UserDs.Any(x => x.EmailId == Email);
            return Json(isUnique, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }


        //--------------Admin Side (Dashboard, Manage Product)----------------------------------
        [Authorize(Roles = "1")]
        public ActionResult AdminDashboard() 
        {
            List<BillDetail> ls = new List<BillDetail>();
            
            foreach (BillDetail p in db.BillDetails)
            {
                BillDetail item = new BillDetail()
                {
                    InvoiceId = p.InvoiceId,
                    UserId = p.UserId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    PostCode = p.PostCode,
                    AddressL = p.AddressL,
                    PhoneN = p.PhoneN,
                    InvoiceDate = p.InvoiceDate
                    
                };
                
                ls.Add(item);
            }
            int t = db.Invoices.Sum(x => x.Bill).Value;
            ReportDetails r = new ReportDetails()
            {
                tInvoice = db.BillDetails.Count(),
                tOrders = db.Orders.Count(),
                tUsers = db.UserDs.Count() - 1,
                turnover = t
            };
            ViewBag.details = r;
          
             return View(ls); 
        }

        [Authorize(Roles = "1")]
        public ActionResult ManageProducts()
        {

            ViewBag.listproducts = db.ProductLists.ToList();
            return View();

        }

        [Authorize(Roles = "1")]
        public ActionResult AddProduct()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddProduct(ProductList model)
        {
            if (ModelState.IsValid)
            {

                int id = userCheck.AddProductL(model);

                if (id > 0)
                {
                    TempData["msg"] = "Product is Added";
                    return View();
                }
                else
                {
                    TempData["msg"] = "Failed to add";
                    return View();
                }
            }
            return View();
        }

        [Authorize(Roles = "1")]
        public ActionResult Edit(int id)
        {
            var product = db.ProductLists.FirstOrDefault(x => x.ProId == id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(ProductList model)
        {


            if (ModelState.IsValid)
            {

                int i = userCheck.EditProductInfo(model);

                if (i > 0)
                {
                    return RedirectToAction("ManageProducts", "UserAccount");
                }
                else
                {
                    ViewBag.failMsg = "Failed to edit";
                    return View();


                }


            }
            return View();

        }

        // GET: Product/Delete/1 (Delete product with ID)
        [Authorize(Roles = "1")]
        public ActionResult Delete(int id)
        {
            ProductList product = db.ProductLists.FirstOrDefault(m => m.ProId == id);

            if (product != null)
            {
                db.ProductLists.Remove(product);
                db.SaveChanges();
                ViewBag.delMsg = "Product is deleted";
            }

            return RedirectToAction("ManageProducts", "UserAdmin");
        }


    }
}