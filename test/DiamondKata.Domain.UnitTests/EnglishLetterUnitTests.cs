using DiamondKata.Domain.Exception;
using DiamondKata.Domain.UnitTests.Extensions;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.Domain.UnitTests;

public class EnglishLetterUnitTests
{
    [Test]
    public void DoesNotThrowAnExceptionWhenAnAlphabeticalCharIsSupplied() =>
        CharExtensions.GetAllEnglishChars()
            .ToList()
            .ForEach(c =>
                Assert.DoesNotThrow(() => new EnglishChar(c)));

    [Test]
    public void ThrowsAnExceptionWhenANonEnglishCharIsSupplied()
    {

        var chars = CharExtensions.GetAllAsciiChars().Except(CharExtensions.GetAllEnglishChars())
            .ToList();

        chars
        .ForEach(c =>
            Assert.Throws<CharIsNotAnEnglishLetterException>(
                () => new EnglishChar(c)));
    }
}