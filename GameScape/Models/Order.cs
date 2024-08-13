namespace GameScape.Models
{
    public class Order
    {


        public int orderId { get; set; }
        public string adress { get; set; }
        public string postalCode{ get; set; }
        public string cardNumber { get; set; }
        public string expiration { get; set; }
        public string cvv{ get; set; }

    }
}
