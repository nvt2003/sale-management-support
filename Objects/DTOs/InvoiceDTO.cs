using System.ComponentModel.DataAnnotations;

namespace Objects.DTOs
{
    public class InvoiceDTO
    {
        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Too long! Max is 50 characters")]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Too long! Max is 100 characters")]
        public string? CustomerAddress { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "TotalAmount must be a positive value.")]
        public decimal TotalAmount { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
