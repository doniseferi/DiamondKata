using DiamondKata.Domain.ValueType;

namespace DiamondKata.DomainService.Factories;

internal interface IOuterPaddingStringFactory
{
    string Create(EnglishChar requestChar, EnglishChar currentRowsChar, PaddingChar outerPaddingChar);
}