using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal interface IInnerPaddingLengthQueryHandler
{
    int Handle(EnglishChar @char);
}