using DiamondKata.Domain.ValueType;

namespace DiamondKata.DomainService.Generators;

internal interface IOuterPaddingStringGenerator
{
    string Generate(EnglishChar requestChar, EnglishChar currentRowsChar, PaddingChar outerPaddingChar);
}