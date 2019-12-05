using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBootcamp32.Model
{
    [Table("TB_M_Transaction")]
    class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int TotalPembayaran { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        
        public Transaction()
        {

        }

        public Transaction(int totalPembayaran, DateTimeOffset transactionDate)
        {
            this.TotalPembayaran = totalPembayaran;
            this.TransactionDate = transactionDate;
        }

    }
}
