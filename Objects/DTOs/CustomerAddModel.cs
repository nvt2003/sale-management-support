using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects.DTOs
{
    public class CustomerAddModel
    {
        public string CustomerName { get; set; }
        [StringLength(20, ErrorMessage = "Max is 20 numbers")]
        [RegularExpression(@"^\d*$", ErrorMessage = "Input must be number!")]
        public string PhoneNumber { get; set; }
    }
}
