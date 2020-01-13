using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartCouponLibrary
{
    public class BuyXGetYFree : Discount, IDiscount
    {
        protected internal BuyXGetYFree()
        {
        }

        public BuyXGetYFree(string name, IList<Product> applicableProducts, int x, int y)
            : base(name)
        {
            ApplicableProducts = applicableProducts;
            X = x;
            Y = y;
        }

        public override OrderBase ApplyDiscount()
        {
            // custom processing
            foreach (LineItem lineItem in OrderBase.LineItems)
            {
                if (ApplicableProducts.Contains(lineItem.Product) && lineItem.Quantity > X)
                {
                    lineItem.DiscountAmount += ((lineItem.Quantity / X) * Y) * lineItem.Product.Price;
                    lineItem.AddDiscount(this);
                }
            }
            return OrderBase;
        }

        public virtual IList<Product> ApplicableProducts { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
    }
}
