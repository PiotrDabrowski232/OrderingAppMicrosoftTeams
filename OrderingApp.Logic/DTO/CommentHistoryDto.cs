using OrderingApp.Data.Models;

namespace OrderingApp.Logic.DTO
{
    public class CommentHistoryDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
    }
}
