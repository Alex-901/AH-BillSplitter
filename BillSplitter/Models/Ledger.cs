using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Models
{
    public class Ledger
    {
        public int ID { get; set; }
        public decimal Amount { get; set; }
        public int PayeeUserId { get; set; }
        public int PayerUserId { get; set; }
    }
}
