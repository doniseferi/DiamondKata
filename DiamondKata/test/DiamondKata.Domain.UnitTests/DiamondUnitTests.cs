using DiamondKata.Domain.ValueType;
using DiamondKata.DomainService.Generators;
using OuterPaddingChar = DiamondKata.Domain.ValueType.PaddingChar;
using InnerPaddingChar = DiamondKata.Domain.ValueType.PaddingChar;
using DiamondKata.DomainService.QueryHandlers;
using DiamondKata.DomainService.Requests;

namespace DiamondKata.Domain.UnitTests;

public class DiamondUnitTests
{
    private IDiamondQueryHandler GetSystemUnderTest() => new DiamondQueryHandler(
        new RowGeneratorQueryHandler(
            new OuterPaddingStringGenerator(
                new OuterPaddingLengthQueryHandler()),
            new InnerPaddingStringGenerator(
                new InnerPaddingLengthQueryHandler())),
        new GetLowerEnglishLettersQueryHandlers());

    private DiamondRequest GetTestRequest() =>
        new(new EnglishChar('Z'), new OuterPaddingChar(' '), new InnerPaddingChar('-'));


    [Test]
    public void FirstAndLastEnglishLetterInTheDiamondHasNoInternalPadding()
    {
        var testRequest = GetTestRequest();

        var lines = GetSystemUnderTest().Handle(testRequest)
            .Split(Environment.NewLine)
            .ToList();

        var innerPadding = testRequest.InnerPaddingChar;

        Assert.That(
            lines.First(),
            Does.Not.Contain(innerPadding.Value));

        Assert.That(lines.Last(),
            Does.Not.Contain(innerPadding.Value));
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