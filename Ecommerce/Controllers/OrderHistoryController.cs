using Ecommerce.Helper;
using Ecommerce.Models;
using Ecommerce.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class OrderHistoryController : Controller
    {
        private OnlineStoreDataEntities3 _context = new OnlineStoreDataEntities3();
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
            var res = SessionHelper.Instance.user;

            //var currentOrder = _context.OrderItems.Where()
            //    await _service.OrderItemList(orderid, res.Id);
            //return View(currentOrder);
            return View();
        }
        //public async JsonResult RemoveOrder(Int64 orderItemId, Int64 orderId)
        //{
        //    DeleteOrderItemModel deleteOrderItem = new DeleteOrderItemModel();
        //    deleteOrderItem.OrderId = orderId;
        //    deleteOrderItem.OrderItemId = orderItemId;

        //    var res = await _service.RemoveOrderItems(deleteOrderItem);

        //    return Json(res, JsonRequestBehavior.AllowGet);
        //}
    }
}