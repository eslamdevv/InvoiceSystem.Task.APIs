using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Application.Features.Invoices.Models
{
    public class CreateInvoiceRequest
    {
        public string InvoiceNumber { get; set; } = null!;
        public DateTime Date { get; set; }
        public List<CreateInvoiceItemRequest> Items { get; set; } = new();
    }

    public class CreateInvoiceItemRequest
    {
        public string ItemCode { get; set; } = null!;

        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
