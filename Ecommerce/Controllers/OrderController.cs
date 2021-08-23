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
        private OnlineStoreDataEntities4 _context = new OnlineStoreDataEntities4();
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
                OrderItem orderItem = new OrderItem();
                order.OrderNumber = GenerateRandom();
                order.OrderedDate = DateTime.Now;
                order.OrderStatus = "Placed";
                order.UserID = user.UserID;
                
                order.Items = new List<OrderItem>();
                var cart = SessionHelper.Instance.CartProducts;
                order.OrderedQty = cart.Products.Count;

                var _order = GetOrder(order);
                if (_order != null)
                {
                   _context.Orders.Add(_order);
                   var res =  _context.SaveChanges();
                    ViewBag.Message = "Order Placed Successfully !";
                    if(res >0)
                    {

                        foreach (var item in cart.Products)
                        {
                            OrderItem custom = new OrderItem()
                            {
                                ItemID = item.Product.ItemID,
                                OrderDate = DateTime.Now,
                                OrderID = _order.OrderID
                            };
                            _context.OrderItems.Add(custom);
                        }
                        _context.SaveChanges();
                    }
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