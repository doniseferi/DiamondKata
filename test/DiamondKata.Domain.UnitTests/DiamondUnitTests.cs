using DiamondKata.DomainService.QueryHandlers;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.Domain.UnitTests;

public class DiamondUnitTests
{
    [Test]
    public void DiamondHas2NMinus1NumberOfAscendingColumns() =>
        GetEnglishCharacters()
        .ToList()
        .ForEach(englishChar =>
        {
            var diamond = GetSystemUnderTest()
                .Handle(englishChar);

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
        });


    [Test]
    public void DiamondHas2NMinus1NumberOfDescendingColumns() =>
        GetEnglishCharacters()
        .ToList()
        .ForEach(englishLetter =>
        {
            var diamond = GetSystemUnderTest()
            .Handle(englishLetter);

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
        });

    [Test]
    public void FirstAndLastEnglishLetterInTheDiamondHasNoInternalPadding() =>
        GetEnglishCharacters()
        .ToList()
        .ForEach(englishLetter =>
        {
            var lines = GetSystemUnderTest().Handle(englishLetter)
            .Split(Environment.NewLine)
            .ToList();

            var innerPadding = '-';

            Assert.That(
                lines.First(),
                Does.Not.Contain(innerPadding));

            Assert.That(lines.Last(),
                Does.Not.Contain(innerPadding));
        });

    [Test]
    public void FirstAndLastEnglishLetterInTheDiamondAreTheSame() =>
        GetEnglishCharactersExceptA()
        .ToList()
        .ForEach(testChar =>
        {
            var lines = GetSystemUnderTest().Handle(testChar)
                .Split(Environment.NewLine)
                .ToList();

            Assert.That(lines.First(), Is.EqualTo(lines.Last()));
        });

    [Test]
    public void LeftAndRightSideExternalPaddingsAreEqualInLength() =>
        GetEnglishCharactersExceptA()
        .ToList()
        .ForEach(englishChar =>
        {
            var lines = GetSystemUnderTest().Handle(englishChar)
            .Split(Environment.NewLine)
            .ToList();

            Assert.That(lines.First(), Is.EqualTo(lines.Last()));
        });

    [Test]
    public void HeightIsEqualToWidth() =>
        GetEnglishCharactersExceptA()
        .ToList()
        .ForEach(englishChar =>
        {
            var diamond = GetSystemUnderTest()
                .Handle(englishChar);

            var lines = diamond.Split(Environment.NewLine);
            var height = lines.Length;
            var length = lines
                .First()
                .ToCharArray()
                .Length;

            Assert.That(height, Is.EqualTo(length));
        });

    [Test]
    public void EachLineHasTheSameNumberOfCharacters() =>
       GetEnglishCharactersExceptA()
        .ToList()
        .ForEach(englishChar =>
        {
            var diamond = GetSystemUnderTest()
                .Handle(englishChar);

            var lines = diamond
                .Split(Environment.NewLine);

            var length = lines
                .Select(x => x.Length)
                .Distinct();

            Assert.That(length.Count(), Is.EqualTo(1));
        });

    [Test]
    public void DiamondsAreVerticallySymmetrical() =>
        GetEnglishCharactersExceptA()
        .ToList()
        .ForEach(englishChar =>
        {
            var diamond = GetSystemUnderTest()
            .Handle(englishChar);

            var lines = diamond
                .Split(Environment.NewLine)
                .ToList();

            var midPoint = (lines.Count / 2);
            var topHalf = lines.Take(midPoint + 1);
            var bottomHalf = lines.Skip(midPoint);
            CollectionAssert.AreEqual(
                topHalf,
                bottomHalf.Reverse());
        });

    [Test]
    public void DiamondsAreHorizontallySymmetrical() =>
        GetEnglishCharactersExceptA()
        .ToList()
        .ForEach(englishChar =>
        {
            var diamond = GetSystemUnderTest()
                .Handle(englishChar);

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
        });

    [Test]
    public void EnglishCharsAreInAscendingOrderFromTheTopHalfOfTheDiamond() =>
        GetEnglishCharacters()
        .ToList()
        .ForEach(englishChar =>
        {
            var diamond = GetSystemUnderTest()
                .Handle(englishChar);

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
        });

    [Test]
    public void EnglishCharsAreInDescendingOrderFromTheBottomHalfOfTheDiamond() =>
        GetEnglishCharacters()
        .ToList()
        .ForEach(englishChar =>
        {
            var diamond = GetSystemUnderTest()
                .Handle(englishChar);

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
        });

    private static IDiamondQueryHandler GetSystemUnderTest() => new DiamondQueryHandler(
        new RowGeneratorQueryHandler(new OuterPaddingQueryHandler(),
            new InnerPaddingQueryHandler()));

    private static IReadOnlyCollection<EnglishChar> GetEnglishCharacters() => Enumerable.Range('A', 26)
        .Select(x => (char)x)
        .Select(x => new EnglishChar(x))
        .ToList();

    private static IReadOnlyCollection<EnglishChar> GetEnglishCharactersExceptA() =>
        GetEnglishCharacters()
        .Skip(1)
        .ToList();
}