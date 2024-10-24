namespace OrderingApp.Data.Models
{
    public class OrderSignups
    {
        public Guid Id { get; set; }
        public Guid SignedUser { get; set; }
        public string UserDisplayName { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
