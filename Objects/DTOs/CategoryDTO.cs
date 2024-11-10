using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Too long! Max is 50 characters")]
        public string CategoryName { get; set; }

        [StringLength(250, ErrorMessage = "Too long! Max is 250 characters")]
        public string? CategoryDescription { get; set; }
    }
}
