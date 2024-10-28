namespace OrderingApp.Data.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public Guid SignupId { get; set; }
        public virtual OrderSignups Signups { get; set; }
    }
}
