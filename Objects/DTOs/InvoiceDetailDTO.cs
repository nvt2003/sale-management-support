using System.ComponentModel.DataAnnotations;

namespace Objects.DTOs
{
    public class InvoiceDetailDTO
    {
        public int InvoiceDetailId { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "ProductId must be 5 characters long.")]
        public string ProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive value.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "UnitPrice must be a positive value.")]
        public decimal UnitPrice { get; set; }
    }
}
