namespace GameScape.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string ThumbnailPath { get; set; }
        public string GameTitle { get; set; }
        public string CoverPhotoPath { get; set; }

        public int Price { get; set; }

        public string Features {  get; set; }
        public string ScreenshotPath{ get; set; }
        public string TrailerPath{ get; set; }

        public string ProductType { get; set; }


        public int Quantity { get; set; }

    }
}
