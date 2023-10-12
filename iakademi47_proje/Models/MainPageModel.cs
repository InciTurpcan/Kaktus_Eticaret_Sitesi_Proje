namespace iakademi47_proje.Models
{
    public class MainPageModel
    {

        public List<Product>? SliderProducts { get; set; }
        public List<Product>? NewProducts { get; set; }

        public Product? Productofday { get; set; }

        public List<Product>? SpecialProducts { get; set; }

        public List<Product>? DiscountedProducts { get; set; }

        public List<Product>? HighLightedProducts { get; set; }

        public List<Product>? TopsellerProducts { get; set; }

        public List<Product>? StarProducts { get; set; }

        public List<Product>? FeaturedProducts { get; set; }

		public List<Product>? NotableProducts { get; set; }

        //ÜRÜN DETAY SAYFASI İÇİN KULLANDIM.
        public Product? ProductDetails { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
        public List<Product>? RelatedProducts { get; set; }

    }
}
