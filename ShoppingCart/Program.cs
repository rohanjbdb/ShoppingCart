using CartCouponLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    class Program
    {
        //For Real time Frameworks We will have to Use Dependancy Injection & Design Patterns Like Factory.
        static void Main(string[] args)
        {
            Cart cart = LoadCart();

            Order order = ProcessCartToOrder(cart);

            // display the cart contents
            foreach (LineItem lineItem in order.LineItems)
            {
                Console.WriteLine("Product: {0}\t Price: {1:c}\t Quantity: {2} \t Subtotal: {4:c} \t Discount: {3:c} \t| Discounts Applied: {5}", lineItem.Product.Name, lineItem.Product.Price, lineItem.Quantity, lineItem.DiscountAmount, lineItem.Subtotal, lineItem.Discounts.Count);                
            }

            Console.ReadKey();
        }

        private static Cart LoadCart()
        {
            // create the cart
            Cart cart = new Cart(new Member("Rohan"));

            // add items to the cart
            GenericProduct Trouser = new GenericProduct("Trouser", 110m);
            cart.AddLineItem(Trouser, 5);

            EventItem race = new EventItem("Ticket", 90m);
            cart.AddLineItem(race, 1);           

            //Add Discount
            //This would generally be Dynamic from rule engine.
            Discount buyXGetY = new BuyXGetYFree("Buy 2 Trousers get 1 Trouser free", new List<Product> { Trouser }, 2, 1);
            buyXGetY.CanBeUsedInJuntionWithOtherDiscounts = false;
            buyXGetY.SupercedesOtherDiscounts = true;
            cart.AddDiscount(buyXGetY);

            return cart;
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
