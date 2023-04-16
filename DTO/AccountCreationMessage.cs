using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AccountCreationMessage
    {
        public int AccountId { get; set; }

        public decimal InitialCredit { get; set; }
    }
}
