using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal interface IGetDiamondQueryHandler
{
    string Handle(EnglishChar @char);
}