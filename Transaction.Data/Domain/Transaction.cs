using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public int AccountId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public short TransactionType { get; set; }

        public DateTime Date { get; set; }
    }
}
