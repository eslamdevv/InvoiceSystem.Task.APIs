using InvoiceSystem.Core;
using InvoiceSystem.Infrastructure._Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _Context;

        public UnitOfWork(AppDbContext Context)
        {
            _Context = Context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await _Context.SaveChangesAsync(ct);
        public ValueTask DisposeAsync()
            => _Context.DisposeAsync();
    }
}
