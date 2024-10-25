namespace OrderingApp.Logic.DTO
{
    public class CommentDto
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public string Comment { get; set; }
        public string UserDisplayname { get; set; }
        public string OrderName { get; set; }
    }
}
