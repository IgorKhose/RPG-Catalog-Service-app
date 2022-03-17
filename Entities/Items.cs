namespace Play.Catalog.Service.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        // DateTimeOffset is a representation of instantaneous time (also known as absolute time)
        public DateTimeOffset CreatedDate { get; set; }

    }
}