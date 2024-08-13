namespace GameScape.Models
{
    public class CartOrder
    {
        public CartItems Cart { get; set; }
        public Order Order { get; set; }



        public CartOrder(CartItems cart, Order order)
        {
            Cart = cart;
            Order = order;
        }



    }
}
