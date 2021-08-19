using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ElectronicsController : Controller
    {
        private OnlineStoreDataEntities _context = new OnlineStoreDataEntities();
        // GET: Electronics
        public ActionResult Index()
        {
            var _prodList = _context.Items.ToList();
            IEnumerable<Item> prodList = _prodList;
            return View(prodList);
        }

        public ActionResult Product(int? Id)
        {
            try
            {
                if(Id >0)
                {
                    Item res = _context.Items.Where(x => x.ItemID == Id).FirstOrDefault();
                    if (res != null)
                    {
                        return View(res);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
            }
            catch
            {
                return HttpNotFound();
            }
            return HttpNotFound();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }
    }
}