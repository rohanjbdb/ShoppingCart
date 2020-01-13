using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartCouponLibrary
{
    public class EventItem : Product
    {
        protected internal EventItem()
        {
        }

        public EventItem(string name, decimal price) : base(name, price)
        {
        }
    }
}
