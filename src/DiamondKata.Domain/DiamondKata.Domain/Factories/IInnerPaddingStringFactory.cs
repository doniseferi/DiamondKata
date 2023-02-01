using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.Factories;

internal interface IInnerPaddingStringFactory
{
    string Create(EnglishChar @char);
}