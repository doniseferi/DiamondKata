using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers.GetPadding;

internal interface IGetInnerPaddingQueryHandler
{
    string Handle(EnglishChar @char);
}