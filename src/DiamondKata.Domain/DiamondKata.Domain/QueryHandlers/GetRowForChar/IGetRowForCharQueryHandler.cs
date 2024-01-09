using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers.GetRowForChar;

internal interface IGetRowForCharQueryHandler
{
    string Handle(EnglishChar @char, EnglishChar lastCharInDiamond);
}