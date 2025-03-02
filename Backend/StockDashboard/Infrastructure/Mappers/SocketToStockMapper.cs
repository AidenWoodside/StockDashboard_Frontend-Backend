using StockDashboard.API.Services.background.models;
using StockDashboard.Domain.Models;

namespace StockDashboard.Infrastructure.Mappers;
using AutoMapper;

public class SocketToStockMapper : Profile
{
    public SocketToStockMapper()
    {
        CreateMap<TradingResponse, Stock>()
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.S))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.p));
        
        CreateMap<QuoteResponse, Stock>()
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.S))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ap));
    }
}