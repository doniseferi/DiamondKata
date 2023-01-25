using DiamondKata.Domain.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal interface IGetLowerEnglishLettersQueryHandlers
{
    IReadOnlyCollection<EnglishChar> Handle(EnglishChar @char);
}