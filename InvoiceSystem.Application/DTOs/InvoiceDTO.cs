using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Application.DTOs
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public DateTime Date { get; set; }
        public List<InvoiceItemDTO> Items { get; set; } = new();

    }
}
