using DiamondKata.Domain.ValueType;

namespace DiamondKata.DomainService.Factories;

internal interface IInnerPaddingStringFactory
{
    string Create(EnglishChar @char, PaddingChar innerPaddingChar);
}