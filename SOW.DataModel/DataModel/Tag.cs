namespace SOW.DataModel
{
    public class Tag
    {
        public long TagId { get; set; }
        public string Name { get; set; }

        public IEnumerable<Product>? Products { get; set; }
    }
}
