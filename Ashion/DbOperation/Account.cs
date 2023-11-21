using Ashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Ashion.DbOperation
{
    public class Account
    {
        AshionEntities5 db = new AshionEntities5();

        public int AddUser(UserSignUp model)
        {

            UserD user = new UserD();
            user.UserName = model.UserName;
            user.EmailId = model.Email;
            user.RoleType = "2";
            user.Position = model.Password;

            db.UserDs.Add(user);
            db.SaveChanges();

            return 1;
        }




        public int AddProductL(ProductList model)
        {
            int id = db.ProductLists.Max(x => x.ProId);
            
            using (var context = new AshionEntities5())
            {

                ProductList product = new ProductList()
                {
                    
                    ProId = id+1,
                    Name = model.Name,
                    Description = model.Description,
                    Unit = model.Unit,
                    Image = model.Image,
                    CatId = model.CatId

                };

                context.ProductLists.Add(product);
                context.SaveChanges();

            }

            return 1;

        }

        public int EditProductInfo(ProductList model)
        {

            ProductList product = db.ProductLists.Where(m => m.ProId == model.ProId).FirstOrDefault();
            using (var context = new AshionEntities5())
            {
                if (product != null)
                {
                    product.Name = model.Name;
                    product.Unit = model.Unit;
                    product.Image = model.Image;
                    product.Description = model.Description;
                    product.CatId = model.CatId;
                    product.ProId = model.ProId;
                    db.SaveChanges();
                }
            }
            return 1;


        }


    }
}