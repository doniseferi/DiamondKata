using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers.GetPadding;

internal interface IGetOuterPaddingQueryHandler
{
    string Handle(EnglishChar @char, EnglishChar lastCharInDiamond);
}