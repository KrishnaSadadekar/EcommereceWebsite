using Ashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Ashion.DbOperation
{
    public class Shopping
    {
        AshionEntities5 db = new AshionEntities5();
        public int InvoiceCreate(InvoiceM model, int bill, int UId)
        {


            using (var context = new AshionEntities5())
            {

                Invoice iv1 = new Invoice()
                {

                    Bill = bill,
                    Payment = model.SelectedPaymentMethod,
                    Contact = model.PhoneNumber,
                    Address = model.Address,
                    UserId = UId,
                    InvoiceDate = System.DateTime.Now

                };
                context.Invoices.Add(iv1);
                context.SaveChanges();



                PersonalDetail p = new PersonalDetail()
                {

                    FirstName = model.Name,
                    LastName = model.LastN,
                    Country = model.Country,
                    State = model.State,
                    City = model.City,
                    AddressL = model.Address,
                    PhoneN = model.PhoneNumber,
                    PostCode = model.PostCode

                };
                context.PersonalDetails.Add(p);
                context.SaveChanges();
            }



            return 1;
        }



        public int placeOrder(List<Cart> li2, int userId)
        {

            //Order Book

            using (var context = new AshionEntities5())
            {
                foreach (var item in li2)
                {

                    Order od = new Order();

                    
                    
                    od.OrderDate = System.DateTime.Now;
                    od.Total = item.bill;
                    od.Unit = item.price;
                    od.Qty = item.qty;
                    od.UserId = userId;
                    od.InvoiceId = context.Invoices.Max(x=>x.InvoiceId);
                    od.Size = item.size;
                    od.ProId = item.proId;
                    context.Orders.Add(od);
                    context.SaveChanges();
                }

            }
            
            return 1;
        }


        
    
    
    }
}