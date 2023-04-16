using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AccountDetailsInfo
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public bool ShowTransaction { get; set; }

        public List<TransactionInfo> Transactions { get; set; }
    }
}
