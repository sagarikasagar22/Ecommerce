using Ecommerce.Helper;
using Ecommerce.Models;
using Ecommerce.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Ecommerce.Controllers
{
    public class EcommerceBaseController : Controller
    {
        //public readonly IVyapar _Service;

        //public ARStoreBaseController()
        //{
        //    _Service = new VyaparService();
        //}

        public void AddProductInCart(Int64 itemId, int quantity)
        {
            CartProduct _cartProduct;
            var _Products = SessionHelper.Instance.CartProducts;

            if (_Products == null)
                _Products = new CartProducts();

            _cartProduct = _Products.Products.FirstOrDefault(x => x.Product.ItemID == itemId);

            var _items = SessionHelper.Instance.Items;
            var _item = _items.FirstOrDefault(x => x.ItemID == itemId);

            if (_cartProduct == null)
            {
                _cartProduct = new CartProduct() { Product = _item, Quantity = quantity };
                _Products.Products.Add(_cartProduct);
            }
            else
            {
                _cartProduct.Quantity = _cartProduct.Quantity + quantity;
            }

            _item.ItemQty = _item.ItemQty + 1;

            SessionHelper.Instance.CartProducts = _Products;
        }

        public void RemoveProducts(Int64 itemId)
        {
            var _Products = SessionHelper.Instance.CartProducts;
            var product = _Products.Products.FirstOrDefault(x => x.Product.ItemID == itemId);

            var _items = SessionHelper.Instance.Items;
            var _item = _items.FirstOrDefault(x => x.ItemID == itemId);

            if (product != null)
            {
                _Products.Products.Remove(product);
                _item.ItemQty = 0;
                _items.Append(_item);
            }
            SessionHelper.Instance.CartProducts = _Products;
            SessionHelper.Instance.Items = _items;
        }

        public void UpdateProductQuantity(Int64 itemId, int quantity)
        {
            var _Products = SessionHelper.Instance.CartProducts;
            var product = _Products.Products.FirstOrDefault(x => x.Product.ItemID== itemId);

            var _items = SessionHelper.Instance.Items;
            var _item = _items.FirstOrDefault(x => x.ItemID == itemId);

            if (product != null)
            {
                product.Quantity = product.Quantity + quantity;
                if (product.Quantity == 0)
                {
                    _Products.Products.Remove(product);
                }
                else { _Products.Products.Append(product); }

                _item.ItemQty = _item.ItemQty + quantity;
                _items.Append(_item);
            }
            //SessionHelper.Instance.CartProducts = _Products;
            //SessionHelper.Instance.Items = _items;
        }

        public void ClearCart()
        {

            foreach (var item in SessionHelper.Instance.CartProducts.Products)
            {
                item.Quantity = 0;
                item.Product.ItemQty = 0;
            }

            SessionHelper.Instance.CartProducts = null;
            SessionHelper.Instance.OrderId = string.Empty;
        }

        public void SetSession(SessionStateStoreData sessionStateStoreData)
        {
            try
            {
                SessionHelper.Instance.user = sessionStateStoreData.Items["LoggedInUser"] as User;
            }
            catch (Exception ex)
            { }
            try
            {
                SessionHelper.Instance.CartProducts = sessionStateStoreData.Items["CartProducts"] as CartProducts;
            }
            catch (Exception ex)
            { }
            try
            {
                SessionHelper.Instance.Items = sessionStateStoreData.Items["Item"] as IEnumerable<CustomItem>;
            }
            catch (Exception ex)
            { }
            try
            {
                SessionHelper.Instance.Orders = sessionStateStoreData.Items["Order"] as IEnumerable<Order>;
            }
            catch (Exception ex)
            { }
            try
            {
                SessionHelper.Instance.OrderId = sessionStateStoreData.Items["OrderId"] as string;
            }
            catch (Exception ex)
            { }

        }

    }
}