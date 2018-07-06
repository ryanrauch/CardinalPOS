using System;

namespace CardinalPOSLibrary.Models
{
    public class Item
    {
        public Guid ItemId { get; set; }
        public string Description { get; set; }
        public Guid ItemGroupId { get; set; }
        public ItemGroup ItemGroup { get; set; }
        public Decimal Price { get; set; }
    }
}
