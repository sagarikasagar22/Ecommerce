using Ecommerce.Models;
using Ecommerce.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Helper
{
    public class SessionHelper
    {
        public static SessionHelper _instance;

        public static SessionHelper Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new SessionHelper();
                }
                return _instance;
            }
        }

        public User user
        {
            get
            {
                var _user = HttpContext.Current.Session["LoggedInUser"] as User;
                return _user;
            }
            set
            {
                HttpContext.Current.Session["LoggedInUser"] = value;
            }

        }

        public CartProducts CartProducts
        {
            get
            {
                var _CartProducts = HttpContext.Current.Session["CartProducts"] as CartProducts;
                return _CartProducts;
            }
            set
            {
                //if(value == null)
                //{
                //    //HttpContext.Current.Cache.Remove("CartProducts");
                //}
                //else
                //{
                HttpContext.Current.Session["CartProducts"] = value;
                //}

            }
        }
        public IEnumerable<CustomItem> Items
        {
            get
            {
                var _items = HttpContext.Current.Session["Item"] as IEnumerable<CustomItem>;
                return _items;
            }
            set
            {
                HttpContext.Current.Session["Item"] = value;
                
            }
        }


        public IEnumerable<Order> Orders
        {
            get
            {
                var _orders = HttpContext.Current.Session["Order"] as IEnumerable<Order>;
                return _orders;
            }
            set
            {
                HttpContext.Current.Session["Order"] = value;
            }
        }

        public IEnumerable<Order> OrderHistory
        {
            get
            {
                var _orders = HttpContext.Current.Session["OrderHistory"] as IEnumerable<Order>;
                return _orders;
            }
            set
            {
                HttpContext.Current.Session["OrderHistory"] = value;
            }
        }

        public string OrderId
        {
            get
            {
                var _orderId = Convert.ToString(HttpContext.Current.Session["OrderId"]);
                return _orderId;
            }
            set
            {
                HttpContext.Current.Session["OrderId"] = value;
            }
        }
    }
}