using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CreateAccountInfo
    {
        [Required]
        public int CustomerId { get; set; }

        //[Range(0, double.MaxValue, ErrorMessage = "InitialCredit must be greater than or equal to 0.")]
        public decimal InitialCredit { get; set; }
    }
}
