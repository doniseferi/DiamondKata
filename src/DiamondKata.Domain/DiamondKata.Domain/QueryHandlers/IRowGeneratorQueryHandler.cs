using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal interface IRowGeneratorQueryHandler
{
    string Handle(EnglishChar @char, EnglishChar lastCharInDiamond);
}