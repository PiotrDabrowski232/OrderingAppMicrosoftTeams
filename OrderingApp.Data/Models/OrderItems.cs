namespace OrderingApp.Data.Models
{
    public class OrderItems
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }

        public Guid DishId { get; set; }
        public virtual Dish Dish { get; set; }
        public Guid SignupId { get; set; }
        public virtual OrderSignups Signup { get; set; }
    }
}
