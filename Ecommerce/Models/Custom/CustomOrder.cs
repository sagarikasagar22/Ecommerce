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
        public int OrderedQty { get; set; }
        public DateTime OrderedDate { get; set; }
        public string OrderStatus { get; set; }
        public int UserID { get; set; }
        public List<OrderItem> Items { get; set; }

    }
}