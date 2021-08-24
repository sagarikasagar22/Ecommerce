using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceWEBAPI.ViewModels
{
    public class ItemViewModel
    {

        public int ItemID { get; set; }
        public int CategoryID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ImagePath { get; set; }
        public decimal ItemPrice { get; set; }
        public int ItemQty { get; set; }

    }
}