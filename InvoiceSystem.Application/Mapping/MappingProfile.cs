using AutoMapper;
using InvoiceSystem.Application.DTOs;
using InvoiceSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InvoiceItem, InvoiceItemDTO>()
                .ForMember(d => d.Total, opt => opt.MapFrom(s => s.Total));

            CreateMap<Invoice, InvoiceDTO>();
        }
    }
}
