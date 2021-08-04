using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Address;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.Domain.Order;
using Auftragsverwaltung.Domain.Position;
using Auftragsverwaltung.Domain.Town;
using AutoMapper;

namespace Auftragsverwaltung.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Customer, CustomerDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<Article, ArticleDto>();
            CreateMap<ArticleGroup, ArticleGroupDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Position, PositionDto>();
            CreateMap<Town, TownDto>();

            CreateMap<ResponseDto<Customer>, CustomerDto>()
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src => src.Entity.Address))
                .ForMember(dest => dest.Orders,
                    opt => opt.MapFrom(src => src.Entity.Orders))
                .ForMember(dest => dest.CustomerId,
                    opt => opt.MapFrom(src => src.Entity.CustomerId))
                .ForMember(dest => dest.Firstname,
                    opt => opt.MapFrom(src => src.Entity.Firstname))
                .ForMember(dest => dest.Lastname,
                    opt => opt.MapFrom(src => src.Entity.Lastname))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Entity.Email))
                .ForMember(dest => dest.Website,
                    opt => opt.MapFrom(src => src.Entity.Website))
                .ForMember(dest => dest.Password,
                    opt => opt.MapFrom(src => src.Entity.Password))
                .ForPath(dest => dest.Response.Flag,
                    opt => opt.MapFrom(src => src.Flag))
                .ForPath(dest => dest.Response.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.Response.Message,
                    opt => opt.MapFrom(src => src.Message))
                .ForPath(dest => dest.Response.NumberOfRows,
                    opt => opt.MapFrom(src => src.NumberOfRows));
            CreateMap<ResponseDto<Order>, OrderDto>()
                .ForMember(dest => dest.CustomerId,
                    opt => opt.MapFrom(src => src.Entity.CustomerId))
                .ForMember(dest => dest.Date,
                    opt => opt.MapFrom(src => src.Entity.Date))
                .ForMember(dest => dest.Customer,
                    opt => opt.MapFrom(src => src.Entity.Customer))
                .ForMember(dest => dest.OrderId,
                    opt => opt.MapFrom(src => src.Entity.OrderId))
                .ForMember(dest => dest.Positions,
                    opt => opt.MapFrom(src => src.Entity.Positions))
                .ForPath(dest => dest.Response.Flag,
                    opt => opt.MapFrom(src => src.Flag))
                .ForPath(dest => dest.Response.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.Response.Message,
                    opt => opt.MapFrom(src => src.Message))
                .ForPath(dest => dest.Response.NumberOfRows,
                    opt => opt.MapFrom(src => src.NumberOfRows));
            CreateMap<ResponseDto<Article>, ArticleDto>()
                .ForMember(dest => dest.ArticleGroup,
                    opt => opt.MapFrom(src => src.Entity.ArticleGroup))
                .ForMember(dest => dest.ArticleGroupId,
                    opt => opt.MapFrom(src => src.Entity.ArticleGroupId))
                .ForMember(dest => dest.ArticleId,
                    opt => opt.MapFrom(src => src.Entity.ArticleId))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => src.Entity.Description))
                .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(src => src.Entity.Position))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Entity.Price))
                .ForPath(dest => dest.Response.Flag,
                    opt => opt.MapFrom(src => src.Flag))
                .ForPath(dest => dest.Response.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.Response.Message,
                    opt => opt.MapFrom(src => src.Message))
                .ForPath(dest => dest.Response.NumberOfRows,
                    opt => opt.MapFrom(src => src.NumberOfRows));

            CreateMap<CustomerDto, Customer>();
            CreateMap<AddressDto, Address>();
            CreateMap<ArticleDto, Article>();
            CreateMap<ArticleGroupDto, ArticleGroup>();
            CreateMap<OrderDto, Order>();
            CreateMap<PositionDto, Position>();
            CreateMap<TownDto, Town>();
        }
    }
}
