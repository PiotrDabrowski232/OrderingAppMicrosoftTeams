namespace OrderingApp.Logic.DTO
{
    public class MenuDto
    {
        public IList<DishDto> Main { get; set; } = new List<DishDto>();
        public IList<DishDto> Extras { get; set; } = new List<DishDto>();
    }
}
