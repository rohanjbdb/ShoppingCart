using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CartCouponLibrary;
using System.Linq;
using System.Collections.Generic;

namespace TestShoppingCart
{
    [TestClass]
    public class TestShoppingCart
    {
        [TestMethod]
        public void TestBuy2GetOneFree()
        {
            //Product Amount = $100
            //Discount Applied is Buy 2 Get One Free
            //Total Quantity in Cart is 5
            //Expected Discount should be $200
            Cart cart = new Cart(new Member("Rohan"));

            // add items to the cart
            GenericProduct Trouser = new GenericProduct("Trouser", 100m);
            cart.AddLineItem(Trouser, 5);

            EventItem race = new EventItem("Ticket", 70m);
            cart.AddLineItem(race, 1);

            //Add Discount
            //This would generally be Dynamic from rule engine.
            Discount buyXGetY = new BuyXGetYFree("Buy 2 Trousers get 1 Trouser free", new List<Product> { Trouser }, 2, 1);
            buyXGetY.CanBeUsedInJuntionWithOtherDiscounts = false;
            buyXGetY.SupercedesOtherDiscounts = true;
            cart.AddDiscount(buyXGetY);

            Order order = ProcessCartToOrder(cart);

            LineItem item = order.LineItems.FirstOrDefault(x => x.Product.Name == "Trouser");

            Assert.IsTrue(item.Subtotal == 300m);
            Assert.IsTrue(item.DiscountAmount == 200m);

        }       

        private static Order ProcessCartToOrder(Cart cart)
        {
            Order order = new Order(cart.Member);
            foreach (LineItem lineItem in cart.LineItems)
            {
                order.AddLineItem(lineItem.Product, lineItem.Quantity);
                foreach (Discount discount in lineItem.Discounts)
                {
                    order.AddDiscount(discount);
                }
            }
            return order;
        }
    }
}
