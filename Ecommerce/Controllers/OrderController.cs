using Ecommerce.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using Ecommerce.Models.Custom;

namespace Ecommerce.Controllers
{
    public class OrderController : EcommerceBaseController
    {
        private OnlineStoreDataEntities3 _context = new OnlineStoreDataEntities3();
        // GET: Order
        public ActionResult Index()
        {
            var user = SessionHelper.Instance.user;

            if (SessionHelper.Instance.user == null)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = "Order", area = "" });
            }
            else
            {
                CustomOrder order = new CustomOrder();
                order.OrderNumber = GenerateRandom();
                order.OrderedDate = DateTime.Now;
                order.OrderStatus = "Placed";
                order.UserID = user.UserID;
                order.CustomerName = user.FirstName + user.LastName;
                
                order.Items = new List<CustomItem>();
                var cart = SessionHelper.Instance.CartProducts;
                order.OrderedQty = cart.Products.Count;
                foreach (var item in cart.Products)
                {
                    CustomItem custom = new CustomItem()
                    {
                        ImagePath = item.Product.ImagePath,
                        ImgSrc = item.Product.ImgSrc,
                        ItemCode = item.Product.ItemCode,
                        ItemDescription = item.Product.ItemDescription,
                        ItemID = item.Product.ItemID,
                        ItemName = item.Product.ItemName,
                        ItemPrice = item.Product.ItemPrice,
                        ItemQty = item.Product.ItemQty,
                        CategoryID = item.Product.CategoryID

                    };
                    order.Items.Add(custom);
                }
                var _order = GetOrder(order);
                if (_order != null)
                {
                    _context.Orders.Add(_order);
                    _context.SaveChanges();
                    ViewBag.Message = "Order Placed Successfully !";
                }
                return View(order);

            }
            return View();
        }
        public static string GenerateRandom()
        {
            Random random = new Random();
            string r = random.Next(0, 999999).ToString("D6");
            return r;
        }

        public Order GetOrder (CustomOrder custom)
        {
            Order order = new Order()
            {
                UserID = custom.UserID,
                OrderID = custom.OrderID,
                OrderedDate = custom.OrderedDate,
                OrderNumber = custom.OrderNumber,
                OrderStatus = custom.OrderStatus,

            };
            return order;
        }
    }
}