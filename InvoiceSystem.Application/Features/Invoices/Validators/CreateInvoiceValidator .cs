using FluentValidation;
using InvoiceSystem.Application.Features.Invoices.Commands;
using InvoiceSystem.Application.Features.Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Application.Features.Invoices.Validators
{
    public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceValidator()
        {
            RuleFor(x => x.Request.InvoiceNumber).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Request.Date).NotEmpty();

            RuleFor(x => x.Request.Items).NotNull().Must(x => x.Count > 0)
                .WithMessage("Invoice must at least 1 item.");

            RuleForEach(x => x.Request.Items).SetValidator(new CreateInvoiceItemValidator());
        }
    }

    public class CreateInvoiceItemValidator : AbstractValidator<CreateInvoiceItemRequest>
    {
        public CreateInvoiceItemValidator()
        {
            RuleFor(x => x.ItemCode).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Qty).GreaterThan(0);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
