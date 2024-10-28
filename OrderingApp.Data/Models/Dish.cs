namespace OrderingApp.Data.Models
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Calories { get; set; }
        public bool IsExtras { get; set; }
        public bool Blocked { get; set; }
        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }  
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
