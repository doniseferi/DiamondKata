using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal interface IOuterPaddingQueryHandler
{
    string Handle(EnglishChar @char, EnglishChar lastCharInDiamond);
}