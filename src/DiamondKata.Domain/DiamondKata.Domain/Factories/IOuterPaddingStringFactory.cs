using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.Factories;

internal interface IOuterPaddingStringFactory
{
    string Create(EnglishChar requestChar, EnglishChar currentRowsChar);
}