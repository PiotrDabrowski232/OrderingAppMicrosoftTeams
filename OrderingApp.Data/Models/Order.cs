namespace OrderingApp.Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float MinValue { get; set; }
        public float DeliveryCost { get; set; }
        public float FreeDeliveryFrom { get; set; }
        public int PhoneNumber { get; set; }
        public decimal BankAccountNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsActive { get; set; }

        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public ICollection<OrderSignups> OrderSignups { get; set; }
    }
}
