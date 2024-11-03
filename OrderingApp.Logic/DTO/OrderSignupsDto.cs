namespace OrderingApp.Logic.DTO
{
    public class OrderSignupsDto
    {
        public Guid Id { get; set; }
        public Guid SignedUser { get; set; }
        public string UserDisplayName { get; set; }
        public bool IsMySignup { get; set; }
        public bool IsPaid { get; set; }
        public float SignupValue { get; set; }
    }
}
