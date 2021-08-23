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
    public class ElectronicsController : EcommerceBaseController
    {
        private OnlineStoreDataEntities3 _context = new OnlineStoreDataEntities3();
        // GET: Electronics
        public ActionResult Index(string Search)
        {
            List<CustomItem> ItemList = new List<CustomItem>();
            CustomItem item = new CustomItem();
            if (Search != null)
            {
                var _ProdList = _context.Items.Where(x => x.ItemName.ToUpper().Contains(Search.ToUpper())).ToList();
                foreach(var prod in _ProdList)
                {
                    string imgPath = Path.Combine("C:/Users/sagarika.sagar/source/repos/Ecommerce/Ecommerce/" + prod.ImagePath);
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
                //IEnumerable<CustomItem> ProdList = (IEnumerable<CustomItem>)_ProdList;
                //return View(ProdList);
                Ecommerce.Helper.SessionHelper.Instance.Items = ItemList;
                return View(ItemList);
            }
            //else if(id == 5)
            //{
            //    var _ProdList = _context.Items.Where(x => x.CategoryID == id).ToList();
            //    foreach (var prod in _ProdList)
            //    {
            //        string imgPath = Path.Combine("C:/Users/sagarika.sagar/source/repos/Ecommerce/Ecommerce/" + prod.ImagePath);
            //        // string imgPath = Server.MapPath(prod.ImagePath);
            //        // Convert image to byte array  
            //        byte[] byteData = System.IO.File.ReadAllBytes(imgPath);
            //        //Convert byte arry to base64string   
            //        string imreBase64Data = Convert.ToBase64String(byteData);
            //        string imgDataURL = string.Format("data:image/jpg;base64,{0}", imreBase64Data);
            //        //Passing image data in viewbag to view  
            //        item = GetItem(prod);
            //        item.ImgSrc = imgDataURL;
            //        ItemList.Add(item);
            //    }
            //    //IEnumerable<CustomItem> ProdList = (IEnumerable<CustomItem>)_ProdList;
            //    //return View(ProdList);
            //    Ecommerce.Helper.SessionHelper.Instance.Items = ItemList;
            //    return View(ItemList);
            //}
            //else if (id == 4)
            //{
            //    var _ProdList = _context.Items.Where(x => x.CategoryID == id).ToList();
            //    foreach (var prod in _ProdList)
            //    {
            //        string imgPath = Path.Combine("C:/Users/sagarika.sagar/source/repos/Ecommerce/Ecommerce/" + prod.ImagePath);
            //        // string imgPath = Server.MapPath(prod.ImagePath);
            //        // Convert image to byte array  
            //        byte[] byteData = System.IO.File.ReadAllBytes(imgPath);
            //        //Convert byte arry to base64string   
            //        string imreBase64Data = Convert.ToBase64String(byteData);
            //        string imgDataURL = string.Format("data:image/jpg;base64,{0}", imreBase64Data);
            //        //Passing image data in viewbag to view  
            //        item = GetItem(prod);
            //        item.ImgSrc = imgDataURL;
            //        ItemList.Add(item);
            //    }
            //    //IEnumerable<CustomItem> ProdList = (IEnumerable<CustomItem>)_ProdList;
            //    //return View(ProdList);
            //    Ecommerce.Helper.SessionHelper.Instance.Items = ItemList;
            //    return View(ItemList);
            //}
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
                Ecommerce.Helper.SessionHelper.Instance.Items = ItemList;
                return View(ItemList);
            }
        }
        private CustomItem GetItem(Item item)
        {
            CustomItem custom = new CustomItem()
            {
                ImagePath = item.ImagePath,
                
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


        public JsonResult AddCart1(Int64 itemId)
        {
            var sessionItems = Ecommerce.Helper.SessionHelper.Instance.Items;
            if(sessionItems != null)
            {
                AddProductInCart(itemId, 1);

            }
            var item = Ecommerce.Helper.SessionHelper.Instance.CartProducts.Products.Where(x => x.Product.ItemID == itemId).FirstOrDefault();
            var cartItemCount = Ecommerce.Helper.SessionHelper.Instance.CartProducts.ItemCount;
            return Json(new { CartItemCount = cartItemCount, ItemCount = item.Quantity, ItemId = itemId }, JsonRequestBehavior.AllowGet);
        }
    }
}