using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreatEmpty.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreatEmpty.Data
{
    public class DutchProfile : Profile
    {
        public DutchProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(d => d.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap()
                .ForMember(i => i.Product, opt => opt.Ignore());
        }

    }
}
