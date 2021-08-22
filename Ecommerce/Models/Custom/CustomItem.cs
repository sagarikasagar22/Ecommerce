using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Custom
{
    public class CustomItem
    {
        public int ItemID { get; set; }
        public int CategoryID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ImagePath { get; set; }
        public string ImgSrc { get; set; }
        public decimal ItemPrice { get; set; }
        public int ItemQty { get; set; }
        public string HideId
        {
            get
            {
                return "hide" + this.ItemID;
            }
        }
        public string ShowId
        {
            get
            {
                return "show" + this.ItemID;
            }
        }

        public string QtyUpdate
        {
            get
            {
                return "qtyUpdate" + this.ItemID;
            }
        }

    }
}