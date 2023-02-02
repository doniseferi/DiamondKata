using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.Factories;

internal interface IOuterPaddingQueryHandler
{
    string Handle(EnglishChar @char, EnglishChar lastCharInDiamond);
}