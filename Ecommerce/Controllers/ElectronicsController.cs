using Ecommerce.Models;
using Ecommerce.Models.Custom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ElectronicsController : Controller
    {
        private OnlineStoreDataEntities2 _context = new OnlineStoreDataEntities2();
        // GET: Electronics
        public ActionResult Index(string Search)
        {
            List<CustomItem> ItemList = new List<CustomItem>();
            CustomItem item = new CustomItem();
            if (Search != null)
            {
                var _ProdList = _context.Items.Where(x => x.ItemName.ToUpper().Contains(Search.ToUpper()) || x.ItemCode.ToUpper().Contains(Search.ToUpper())).ToList();
                IEnumerable<CustomItem> ProdList = (IEnumerable<CustomItem>)_ProdList;
                return View(ProdList);
            }
            else
            {
                var _prodList = _context.Items.ToList();
               
                
                foreach (var prod in _prodList)
                {
                    string imgPath = Path.Combine("C:/Users/sagarika.sagar/source/repos/Ecommerce/Ecommerce/"+prod.ImagePath);
                   // string imgPath = Server.MapPath(prod.ImagePath);
                    // Convert image to byte array  
                    byte[] byteData = System.IO.File.ReadAllBytes(imgPath);
                    //Convert byte arry to base64string   
                    string imreBase64Data = Convert.ToBase64String(byteData);
                    string imgDataURL = string.Format("data:image/jpg;base64,{0}", imreBase64Data);
                    //Passing image data in viewbag to view  
                    item = GetItem(prod);
                    item.ImgSrc = imgDataURL;
                    ItemList.Add(item);
                }
                //ViewBag.ImageData = imgDataURL;
                return View(ItemList);
            }
        }
        private CustomItem GetItem(Item item)
        {
            CustomItem custom = new CustomItem()
            {
                ImagePath = item.ImagePath,
                ItemCode = item.ItemCode,
                ItemDescription = item.ItemDescription,
                ItemID = item.ItemID,
                CategoryID = item.CategoryID,
                ItemName = item.ItemName,
                ItemPrice = item.ItemPrice,
                ItemQty = item.ItemQty
            };
            return custom;
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