using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Core.Entities
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public string ItemCode { get; set; } = null!;
        public int Qty { get; set; }
        public decimal Price { get; set; }

        public decimal Total => Qty * Price;

        public int InvoiceId { get; set; }//FK
        public Invoice Invoice { get; set; } = null!;//Navigational Property [One]

    }
}
