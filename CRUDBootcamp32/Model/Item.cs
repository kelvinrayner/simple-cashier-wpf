using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBootcamp32.Model
{
    [Table ("TB_M_Item")]
    class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        
        public Supplier Supplier { get; set; }

        public Item()
        {

        }

        public Item(string name, int stock, int price, Supplier supplierId)
        {
            this.Name = name;
            this.Stock = stock;
            this.Price = price;
            this.Supplier = supplierId;
        }
    }
}
