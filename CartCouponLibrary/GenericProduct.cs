using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartCouponLibrary
{
    public class GenericProduct : Product
    {
        protected internal GenericProduct()
        {
        }

        public GenericProduct(String name, Decimal price) : base(name, price)
        {
        }
    }
}
