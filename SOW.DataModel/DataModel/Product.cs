namespace SOW.DataModel
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public long CategoryId { get; set; }

        /*[JsonIgnore]*/ //проверить
        public Category Category { get; set; }

        //[JsonIgnore]
        public IEnumerable<Tag>? Tags { get; set; }

        public IEnumerable<Comment>? Comments { get; set; }
    }
}
