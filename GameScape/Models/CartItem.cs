namespace GameScape.Models
{
 
        public class CartItems
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            
            public string Image { get; set; }
            public string Category { get; set; }
            public int Quantity { get; set; }
        }
    
}
