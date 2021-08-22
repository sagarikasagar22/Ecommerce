using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Custom
{
    public class CustomOrder
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public int ItemID { get; set; }
        public int OrderedQty { get; set; }
        public System.DateTime OrderedDate { get; set; }
        public string OrderStatus { get; set; }
        public Nullable<int> UserID { get; set; }
        public string CustomerName { get; set; }
        public List<CustomItem> Items { get; set; }
    }
}