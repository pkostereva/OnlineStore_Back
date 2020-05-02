namespace OnlineStoreBack.DB.Models
{
    public class City
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool IsForeign { get; set; }
        public string Product { get; set; }
        public decimal Money { get; set; }
    }
}