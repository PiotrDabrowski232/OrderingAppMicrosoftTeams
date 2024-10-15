using System.ComponentModel.DataAnnotations;

namespace OrderingApp.Logic.DTO
{
    public class OrderDto
    {
        [Required(ErrorMessage = "Order name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Restaurant selection is required.")]
        public string RestaurantId { get; set; }

        [Required(ErrorMessage = "Min Value is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Minimum value must be greater than 0.")]
        public float MinValue { get; set; }

        [Required(ErrorMessage = "Delivery Cost is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Delivery cost must be non-negative.")]
        public float DeliveryCost { get; set; }

        [Required(ErrorMessage = "Free Delivery From is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Free delivery must be greater than or equal to 0.")]
        public float FreeDeliveryFrom { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be 9 digits.")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Bank account number is required.")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Bank account number must be between 10 and 15 digits.")]
        public long BankAccountNumber { get; set; }
        public bool Notifications { get; set; }
        public Guid CreatedBy { get; set; }

    }
}
