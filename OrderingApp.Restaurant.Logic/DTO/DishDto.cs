namespace OrderingApp.Restaurant.Logic.DTO
{
    public class DishDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Calories { get; set; }
        public bool IsExtras { get; set; }
        public bool Blocked { get; set; }
    }
}
