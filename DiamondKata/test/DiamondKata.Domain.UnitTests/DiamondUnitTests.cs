using DiamondKata.Domain.ValueType;
using ExternalPaddingChar = DiamondKata.Domain.ValueType.PaddingChar;
using InternalPaddingChar = DiamondKata.Domain.ValueType.PaddingChar;

namespace DiamondKata.Domain.UnitTests;

public class DiamondUnitTests
{
    [Test]
    public void FirstAndLastEnglishLetterInTheDiamondHasNoInternalPadding()
    {
        var internalPadding = new InternalPaddingChar('-');
        var externalPadding = new ExternalPaddingChar(' ');
        var letter = new EnglishChar('Z');
        
        var lines =
            new Diamond(letter, internalPadding, externalPadding)
                .Value
                .Split(Environment.NewLine)
                .ToList();

        Assert.That(
            lines.First(),
            Does.Not.Contain(internalPadding.Value));

        Assert.That(lines.Last(),
            Does.Not.Contain(internalPadding.Value));
    }

    [Test]
    public void FirstAndLastEnglishLetterInTheDiamondAreTheSame()
    {
        var internalPadding = new InternalPaddingChar('-');
        var externalPadding = new ExternalPaddingChar(' ');
        var letterA = new EnglishChar('Z');

        var diamond =
            new Diamond(letterA, internalPadding, externalPadding)
                .Value
                .Split(Environment.NewLine)
                .ToList();

        Assert.That(diamond.First(), Is.EqualTo(diamond.Last()));
    }

    [Test]
    public async Task LeftAndRightSideExternalPaddingsAreEqualInLength()
    {
        var internalPadding = new InternalPaddingChar('-');
        var externalPadding = new ExternalPaddingChar(' ');
        var letterA = new EnglishChar('Z');

        var lines = 
            new Diamond(letterA, internalPadding, externalPadding)
            .Value
            .Split(Environment.NewLine);

        Assert.That(lines.First(), Is.EqualTo(lines.Last()));
    }

    [Test]
    public void HeightIsEqualToWidth()
    {
        var internalPadding = new InternalPaddingChar('-');
        var externalPadding = new ExternalPaddingChar(' ');
        var letterA = new EnglishChar('Z');

        var diamond = new Diamond(letterA, internalPadding, externalPadding);

        var lines = diamond.Value.Split(Environment.NewLine);
        var height = lines.Length;
        var length = lines
            .First()
            .ToCharArray()
            .Length;

        Assert.That(height, Is.EqualTo(length));
    }

    [Test]
    public void EachLineHasTheSameNumberOfCharacters()
    {
        var internalPadding = new InternalPaddingChar('-');
        var externalPadding = new ExternalPaddingChar(' ');
        var letterA = new EnglishChar('Z');

        var diamond = new Diamond(letterA, internalPadding, externalPadding);

        var lines = diamond.Value.Split('\n');
        var length = lines
            .Select(x => x.Length)
            .Distinct();

        Assert.That(length.Count(), Is.EqualTo(2));
    }

    [Test]
    public void DiamondsAreVerticallySymmetrical()
    {
        var internalPadding = new InternalPaddingChar('-');
        var externalPadding = new ExternalPaddingChar(' ');
        var letterA = new EnglishChar('Z');

        var diamond = new Diamond(letterA, internalPadding, externalPadding);

        var lines = diamond
            .Value
            .Split(Environment.NewLine)
            .ToList();

            var midPoint = (lines.Count / 2);
            var topHalf = lines.Take(midPoint + 1);
            var bottomHalf = lines.Skip(midPoint);
            CollectionAssert.AreEqual(
                topHalf,
                bottomHalf.Reverse());
    }

    [Test]
    public void DiamondsAreHorizontallySymmetrical()
    {
        var internalPadding = new InternalPaddingChar('-');
        var externalPadding = new ExternalPaddingChar(' ');
        var letterA = new EnglishChar('Z');

        var diamond = new Diamond(letterA, internalPadding, externalPadding);

        var lines = diamond
            .Value
            .Split(Environment.NewLine)
            .ToList();

        lines.ForEach(line =>
        {
            var midPoint = line.Length / 2;
            var leftHalf = line.Substring(0, midPoint + 1);
            var rightHalf = line.Substring(midPoint);

            CollectionAssert.AreEqual(leftHalf, rightHalf.Reverse());
        });

    }

    [Test]
    public void EnglishCharsAreInAscendingOrderFromTheTopHalfOfTheDiamond()
    {
        var internalPadding = new InternalPaddingChar('-');
        var externalPadding = new ExternalPaddingChar(' ');
        var letterA = new EnglishChar('Z');

        var diamond = new Diamond(letterA, internalPadding, externalPadding);

        var lines = diamond
            .Value
            .Split(Environment.NewLine);

        var midPoint = (lines.Length / 2) + 1;

        var topHalf =
            lines
                .Take(midPoint)
                .SelectMany(x => x.ToCharArray().Distinct())
                .Where(char.IsLetter)
                .ToList();

        Assert.That(topHalf, Is.Ordered.Ascending);
    }

    [Test]
    public void EnglishCharsAreInDescendingOrderFromTheBottomHalfOfTheDiamond()
    {
        var internalPadding = new InternalPaddingChar('-');
        var externalPadding = new ExternalPaddingChar(' ');
        var letterA = new EnglishChar('Z');

        var diamond = new Diamond(letterA, internalPadding, externalPadding);

        var lines = diamond
            .Value
            .Split(Environment.NewLine);

        var midPoint = (lines.Length / 2);

        var bottomHalf =
            lines
                .Skip(midPoint)
                .SelectMany(x => x.ToCharArray().Distinct())
                .Where(char.IsLetter)
                .ToList();

        Assert.That(bottomHalf, Is.Ordered.Descending);
    }
}