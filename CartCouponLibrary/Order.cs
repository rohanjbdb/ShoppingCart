using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartCouponLibrary
{
    public class Order : OrderBase
    {
        protected internal Order() { }

        public Order(Member member)
            : base(member)
        {
        }
    }
}
