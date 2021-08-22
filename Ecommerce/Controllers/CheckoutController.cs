using Ecommerce.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class CheckoutController : EcommerceBaseController
    {
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }
      


        public JsonResult RemoveProduct(Int64 id)
        {
            RemoveProducts(id);

            return Json(SessionHelper.Instance.CartProducts, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveQuantity(Int64 id)
        {
            UpdateProductQuantity(id, -1);
            var cartItemCount = SessionHelper.Instance.CartProducts.ItemCount;
            //return Json(cartItemCount,JsonRequestBehavior.AllowGet);
            int itemQuantity = 0;
            var item = SessionHelper.Instance.CartProducts.Products.Where(x => x.Product.ItemID == id).FirstOrDefault();
            if (item != null)
            {
                itemQuantity = item.Quantity;
            }
            return Json(new { CartItemCount = cartItemCount, ItemCount = itemQuantity, ItemId = id, }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddQuantity(Int64 id)
        {
            UpdateProductQuantity(id, 1);
            var cartItemCount = SessionHelper.Instance.CartProducts.ItemCount;
            //return Json(cartItemCount,JsonRequestBehavior.AllowGet);
            var item = SessionHelper.Instance.CartProducts.Products.Where(x => x.Product.ItemID == id).FirstOrDefault();
            return Json(new { CartItemCount = cartItemCount, ItemCount = item.Quantity, ItemId = id, }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Payment()
        {
            return View();
        }
    }
}