using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal interface IInnerPaddingQueryHandler
{
    string Handle(EnglishChar @char);
}