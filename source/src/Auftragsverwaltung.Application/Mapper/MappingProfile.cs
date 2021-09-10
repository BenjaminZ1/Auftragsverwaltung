using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Extensions;
using Auftragsverwaltung.Domain.Address;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.Domain.Order;
using Auftragsverwaltung.Domain.Position;
using Auftragsverwaltung.Domain.Town;
using AutoMapper;
using System;
using System.Linq;

namespace Auftragsverwaltung.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Password,
                    opt => opt.Ignore())
                .ForMember(dest => dest.ValidAddress,
                    opt => opt.MapFrom(src => src.Addresses
                        .FirstOrDefault(e => e.ValidUntil == DateTime.MaxValue)));

            CreateMap<Address, AddressDto>();
            CreateMap<Article, ArticleDto>();
            CreateMap<ArticleGroup, ArticleGroupDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Position, PositionDto>();
            CreateMap<Town, TownDto>();

            CreateMap<ResponseDto<Customer>, CustomerDto>()
                .ForMember(dest => dest.ValidAddress,
                    opt => opt.MapFrom(src => src.Entity.Addresses
                        .FirstOrDefault(e => e.ValidUntil == DateTime.MaxValue)))
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
                    opt => opt.Ignore())
                .ForMember(dest => dest.CustomerNumber,
                    opt => opt.MapFrom(src => src.Entity.CustomerNumber))
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
                    opt => opt.MapFrom(src => src.Entity.Positions))
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

            CreateMap<ResponseDto<ArticleGroup>, ArticleGroupDto>()
                .ForMember(dest => dest.Articles,
                    opt => opt.MapFrom(src => src.Entity.Articles))
                .ForMember(dest => dest.ArticleGroupId,
                    opt => opt.MapFrom(src => src.Entity.ArticleGroupId))
                .ForMember(dest => dest.ParentArticleGroup,
                    opt => opt.MapFrom(src => src.Entity.ParentArticleGroup))
                .ForMember(dest => dest.ChildArticlesGroups,
                    opt => opt.MapFrom(src => src.Entity.ChildArticlesGroups))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Entity.Name))
                .ForPath(dest => dest.Response.Flag,
                    opt => opt.MapFrom(src => src.Flag))
                .ForPath(dest => dest.Response.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.Response.Message,
                    opt => opt.MapFrom(src => src.Message))
                .ForPath(dest => dest.Response.NumberOfRows,
                    opt => opt.MapFrom(src => src.NumberOfRows));

            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.Password,
                    opt => opt.MapFrom(src => SecurityHelper
                        .HashPassword(src.Password.ToString(), SecurityHelper.GenerateSalt(70), 42042, 70)));


            CreateMap<AddressDto, Address>();
            CreateMap<ArticleDto, Article>();
            CreateMap<ArticleGroupDto, ArticleGroup>();
            CreateMap<OrderDto, Order>();
            CreateMap<PositionDto, Position>();
            CreateMap<TownDto, Town>();
        }
    }
}
