using InvoiceSystem.Core.Entities;
using InvoiceSystem.Core.Repositories.Contract;
using InvoiceSystem.Infrastructure._Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken ct = default)
            => await _context.Invoices
                .Include(i => i.Items)
                .AsNoTracking()
                .ToListAsync(ct);

        public async Task<Invoice?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id, ct);
        public async Task AddAsync(Invoice invoice, CancellationToken ct = default)
            => await _context.Invoices.AddAsync(invoice, ct).AsTask();


        public void Update(Invoice invoice)
            => _context.Invoices.Update(invoice);
        public void Delete(Invoice invoice)
            => _context.Invoices.Remove(invoice);

    }
}
