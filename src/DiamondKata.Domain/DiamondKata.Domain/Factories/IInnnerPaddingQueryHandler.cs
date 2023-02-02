using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.Factories;

internal interface IInnnerPaddingQueryHandler
{
    string Handle(EnglishChar @char);
}