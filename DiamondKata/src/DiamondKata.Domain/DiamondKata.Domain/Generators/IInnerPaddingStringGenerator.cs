using DiamondKata.Domain.ValueType;

namespace DiamondKata.DomainService.Generators;

internal interface IInnerPaddingStringGenerator
{
    string Generate(EnglishChar @char, PaddingChar innerPaddingChar);
}