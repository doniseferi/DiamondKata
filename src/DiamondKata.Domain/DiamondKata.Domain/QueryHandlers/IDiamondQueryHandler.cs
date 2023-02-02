using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal interface IDiamondQueryHandler
{
    string Handle(EnglishChar @char);
}