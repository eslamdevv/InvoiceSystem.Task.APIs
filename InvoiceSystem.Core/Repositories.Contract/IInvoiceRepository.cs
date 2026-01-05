using InvoiceSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Core.Repositories.Contract
{
    public interface IInvoiceRepository
    {
        Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken ct = default);
        Task<Invoice?> GetByIdAsync(int id, CancellationToken ct = default);

        Task AddAsync(Invoice invoice, CancellationToken ct = default);

        void Update(Invoice invoice);
        void Delete(Invoice invoice);

    }
}
