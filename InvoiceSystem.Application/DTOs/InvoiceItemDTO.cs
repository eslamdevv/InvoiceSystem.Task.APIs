using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Application.DTOs
{
    public class InvoiceItemDTO
    {
        public int Id { get; set; }
        public string ItemCode { get; set; } = null!;
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
