using DiamondKata.Domain.Exception;
using DiamondKata.Domain.UnitTests.Extensions;
using DiamondKata.Domain.ValueType;

namespace DiamondKata.Domain.UnitTests;

public class PaddingUnitTests
{
    [Test]
    public void ThrowsAnExceptionWhenANonPrintableCharIsUsed() =>
        CharExtensions
            .GetAllChars()
            .Except(CharExtensions.GetAllPrintableChars())
            .ToList()
            .ForEach(c =>
                Assert.Throws<CharIsNotAWrittenSymbolException>(() => new PaddingChar(c)));

    [Test]
    public void DoesNotThrowWhenAPrintableCharIsUsed() =>
        CharExtensions.GetAllPrintableChars()
            .ToList()
            .ForEach(c =>
                Assert.DoesNotThrow(
                    () => new PaddingChar(c)));
}