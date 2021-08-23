using Ecommerce.Helper;
using Ecommerce.Models;
using Ecommerce.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Ecommerce.Controllers
{
    public class OrderHistoryController : Controller
    {
        private OnlineStoreDataEntities4 _context = new OnlineStoreDataEntities4();
        // GET: OrderHistory
        public ActionResult Index()
        {
            var res = SessionHelper.Instance.user;
            var orders = _context.Orders.Where(x=>x.UserID == res.UserID).ToList();
            SessionHelper.Instance.OrderHistory = orders;
            return View();
        }
        public ActionResult Items(Int64 orderid)
        {
            CustomItem product = new CustomItem();
            CustomOrder _current = new CustomOrder();
            var res = SessionHelper.Instance.user;

            var currentOrder = _context.Orders.Where(x => x.OrderID == orderid).ToList();
            foreach (var order in currentOrder)
            {
                 _current = GetOrder(order);
                _current.Items = _context.OrderItems.Include(x => x.Item).Where(x => x.OrderID == orderid).ToList();
                
            }
            return View(_current);
        }
        public CustomOrder GetOrder(Order custom)
        {
            CustomOrder order = new CustomOrder()
            {
                UserID = (int)custom.UserID,
                OrderID = custom.OrderID,
                OrderedDate = custom.OrderedDate,
                OrderNumber = custom.OrderNumber,
                OrderStatus = custom.OrderStatus,

            };
            return order;
        }
        public JsonResult RemoveOrder(int? orderItemId, int? orderId)
        {

                //var res =_context.Orders.Where(x=>x.OrderID == orderId).FirstOrDefault();
                //var result = _context.Orders.Remove(res);
             
                var res = _context.OrderItems.Where(x => x.ID == orderItemId).FirstOrDefault();
                var result = _context.OrderItems.Remove(res);
            

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}