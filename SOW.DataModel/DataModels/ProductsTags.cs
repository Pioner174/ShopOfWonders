namespace SOW.DataModels
{
    public class ProductsTags
    {
        public long ProductId { get; set; }

        public Product Product { get; set; }

        public long TagId { get; set; }

        public Tag  Tag { get; set; }

    }
}
