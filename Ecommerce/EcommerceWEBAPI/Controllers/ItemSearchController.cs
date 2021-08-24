using EcommerceWEBAPI.Models;
using EcommerceWEBAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EcommerceWEBAPI.Controllers
{
    public class ItemSearchController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetItemsBySearch(string Search)
        {
            IList<ItemViewModel> items = null;
            using (var x = new OnlineStoreDataEntities())
            {
                items = x.Items.Select(y => new ItemViewModel()
                {
                    ImagePath = y.ImagePath,
                    CategoryID = y.CategoryID,
                    ItemDescription = y.ItemDescription,
                    ItemID = y.ItemID,
                    ItemName = y.ItemName,
                    ItemPrice = y.ItemPrice,
                    ItemQty = y.ItemQty

                }).Where(m => m.ItemName.ToUpper() == Search.ToUpper()).ToList<ItemViewModel>();
            }
            if (items.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(items);
            }
        }

    }
}
