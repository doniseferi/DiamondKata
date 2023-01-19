namespace DiamondKata.Domain.UnitTests;

public class DiamondUnitTests
{
    [Test]
    public void ThrowsAnExceptionWhenANonAlphabeticalCharIsSupplied()
    {
        var nonLetterChars = Enumerable.Range(0, char.MaxValue + 1)
            .Select(i => (char) i)
            .Where(c => !char.IsControl(c) && !char.IsLetter(c))
            .ToList();

        nonLetterChars.ForEach(c => Assert.Throws<CharIsNotALetterException>(() => new Diamond(new Letter(c))));
    }

    [Test]
    public void DoesNotThrowAnExceptionWhenAmAlphabeticalCharIsSupplied()
    {
        var alphabeticalChars = Enumerable.Range(0, char.MaxValue + 1)
            .Select(i => (char)i)
            .Where(c => char.IsLetter(c))
            .ToList();

        alphabeticalChars.ForEach(c => Assert.DoesNotThrow(() => new Diamond(new Letter(c))));
    }
}