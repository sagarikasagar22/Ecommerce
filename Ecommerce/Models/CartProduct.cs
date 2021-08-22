using Ecommerce.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class CartProduct
    {
        public CustomItem Product { get; set; }
        public int Quantity { get; set; }

        public double TotalPrice
        {
            get
            {
                var _totalPrice = Quantity * this.Product.ItemPrice;
                return (double)_totalPrice;
            }
        }
    }

    public class CartProducts
    {

        public CartProducts()
        {
            _Products = new List<CartProduct>();
        }

        List<CartProduct> _Products;
        public List<CartProduct> Products
        {
            get
            {
                return _Products;
            }
            set
            {
                _Products = value;
            }
        }
        public string DiscountCode { get; set; }
        //public CustomerModel DeliveryAddress { get; set; }
        public double Amount
        {
            get
            {
                double sum = 0;
                foreach (var p in this.Products)
                {
                    sum = sum + p.TotalPrice;
                }
                return sum;
            }
        }

        public double Discount
        {
            get; set;
        }

        public double ShippingCharge
        {
            get
            {
                double _shippingCharge = 0;
                if (Ecommerce.Helper.SessionHelper.Instance.user != null)
                {
                    var deliveryAddress =Ecommerce.Helper.SessionHelper.Instance.user;
                    //if(!(deliveryAddress.PinCode == "122006" && deliveryAddress.Address.ToLower().Contains("ramprastha")))
                    //{
                    if (this.Amount <= 100)
                    {
                        _shippingCharge = 80;
                    }
                    else if (this.Amount < 300)
                    {
                        _shippingCharge = 40;
                    }
                    //}
                }
                //if(Amount<200)
                //{
                //    _shippingCharge = 50;
                //}
                //else if (Amount < 500)
                //{
                //    _shippingCharge = 80;
                //}
                return _shippingCharge;
            }
        }

        public double PayableAmount
        {
            get
            {
                double _payable = 0;
                _payable = this.Amount + this.ShippingCharge - this.Discount;

                return _payable;
            }
        }

       
        //private int _ItemCount = 0;

        public int ItemCount
        {
            get
            {
                int _count = 0;
                //foreach (var p in this.Products)
                //{
                //    _count = _count + p.Quantity;
                //}

                this.Products.ForEach(c => _count += c.Quantity);

                return _count;
            }
            //set
            //{
            //    int _count = 0;
            //    this.Products.ForEach(c => _count += c.Quantity);
            //    _ItemCount = _count;
            //}
        }
    }

}