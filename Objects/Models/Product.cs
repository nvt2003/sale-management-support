using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Objects.Models
{
    public class Product
    {
        [Key]
        [StringLength(5, ErrorMessage = "Product Code must be 5 characters long.")]
        public string ProductCode { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Too long! Max is 50 characters")]
        public string ProductName { get; set; }

        [StringLength(250, ErrorMessage = "Too long! Max is 250 characters")]
        public string? ProductDescription { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public string? UserId;
        public virtual Category? Category { get; set; }
    }
}
