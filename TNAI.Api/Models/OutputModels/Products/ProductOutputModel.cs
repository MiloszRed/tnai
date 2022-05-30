namespace TNAI.Api.Models.OutputModels.Products
{
    public class ProductOutputModel
    {
        public int    Id    { get; set; }
        public string Name  { get; set; }
        public int    Price { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}