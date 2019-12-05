using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBootcamp32.Model
{
    [Table("TB_M_TransactionItem")]
    class TransactionItem
    {
        [Key]
        public int Id { get; set; }
        public int Qty { get; set; }
        public int TotalPrice { get; set; }
        
        public Transaction Transaction { get; set; }

        public Item Item { get; set; }

        public TransactionItem()
        {

        }

        public TransactionItem(int qty, int total, Transaction transaction, Item item)
        {
            this.Qty = qty;
            this.TotalPrice = total;
            this.Transaction = transaction;
            this.Item = item;
        }
    }
}
