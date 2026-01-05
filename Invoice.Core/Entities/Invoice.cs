using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Core.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public DateTime Date { get; set; }

        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

    }
}
