namespace OrderingApp.Restaurant.Logic.DTO
{
    public class NewDishDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Calories { get; set; }
        public bool IsExtras { get; set; }
    }
}
