using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Objects.Models
{
    public class InvoiceDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceDetailId { get; set; }
        
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; } 

        [ForeignKey("Product")]
        [Required]
        [StringLength(5, ErrorMessage = "ProductId must be 5 characters long.")]
        public string ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive value.")]
        public int Quantity { get; set; } 

        [Range(0, double.MaxValue, ErrorMessage = "UnitPrice must be a positive value.")]
        public decimal UnitPrice { get; set; }
        public string? UserId;
        [JsonIgnore]
        public virtual Invoice? Invoice { get; set; } 
        public virtual Product? Product { get; set; } 
    }
}
