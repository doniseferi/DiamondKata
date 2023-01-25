using DiamondKata.Domain.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal interface IOuterPaddingLengthQueryHandler
{
    int Handle(EnglishChar requestChar, EnglishChar @char);
}