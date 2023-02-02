using DiamondKata.DomainService.QueryHandlers;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.Domain.UnitTests;

public class DiamondUnitTests
{
    private static IDiamondQueryHandler GetSystemUnderTest() => new DiamondQueryHandler(
        new RowGeneratorQueryHandler(
            new OuterPaddingQueryHandler(),
            new InnerPaddingQueryHandler()),
        new GetLowerEnglishLettersQueryHandlers());

    private EnglishChar GetTestRequest() => new('Z');

    [Test]
    public void FirstAndLastEnglishLetterInTheDiamondHasNoInternalPadding()
    {
        var testRequest = GetTestRequest();

        var lines = GetSystemUnderTest().Handle(testRequest)
            .Split(Environment.NewLine)
            .ToList();

        var innerPadding = '-';

        Assert.That(
            lines.First(),
            Does.Not.Contain(innerPadding));

        Assert.That(lines.Last(),
            Does.Not.Contain(innerPadding));
    }

    [Test]
    public void FirstAndLastEnglishLetterInTheDiamondAreTheSame()
    {
        var testRequest = GetTestRequest();

        var lines = GetSystemUnderTest().Handle(testRequest)
            .Split(Environment.NewLine)
            .ToList();

        Assert.That(lines.First(), Is.EqualTo(lines.Last()));
    }

    [Test]
    public void LeftAndRightSideExternalPaddingsAreEqualInLength()
    {
        var lines = GetSystemUnderTest().Handle(GetTestRequest())
            .Split(Environment.NewLine)
            .ToList();

        Assert.That(lines.First(), Is.EqualTo(lines.Last()));
    }

    [Test]
    public void HeightIsEqualToWidth()
    {
        var diamond = GetSystemUnderTest()
            .Handle(GetTestRequest());

        var lines = diamond.Split(Environment.NewLine);
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
        var diamond = GetSystemUnderTest()
            .Handle(GetTestRequest());

        var lines = diamond
            .Split(Environment.NewLine);

        var length = lines
            .Select(x => x.Length)
            .Distinct();

        Assert.That(length.Count(), Is.EqualTo(1));
    }

    [Test]
    public void DiamondsAreVerticallySymmetrical()
    {
        var diamond = GetSystemUnderTest()
            .Handle(GetTestRequest());

        var lines = diamond
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
        var diamond = GetSystemUnderTest()
            .Handle(GetTestRequest());

        var lines = diamond
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
        var diamond = GetSystemUnderTest()
            .Handle(GetTestRequest());

        var lines = diamond
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
        var diamond = GetSystemUnderTest()
            .Handle(GetTestRequest());

        var lines = diamond
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