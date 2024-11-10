using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Too long! Max is 50 characters")]
        public string CategoryName { get; set; }

        [StringLength(250, ErrorMessage = "Too long! Max is 250 characters")]
        public string? CategoryDescription { get; set; }
        public string? UserId;
    }
}
