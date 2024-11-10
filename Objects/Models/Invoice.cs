using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; } 

        [Required]
        [StringLength(50, ErrorMessage = "Too long! Max is 50 characters")]
        public string? CustomerName { get; set; } 

        [StringLength(100, ErrorMessage = "Too long! Max is 100 characters")]
        public string? CustomerAddress { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "TotalAmount must be a positive value.")]
        public decimal TotalAmount { get; set; } 

        [Required]
        public string Status { get; set; }
        [ForeignKey("Customer")] 
        public int? CustomerId { get; set; }
        public string? UserId;

        public virtual Customer? Customer { get; set; }

        public virtual ICollection<InvoiceDetail>? InvoiceDetails { get; set; } 
    }
}
