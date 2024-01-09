using DiamondKata.Domain.UnitTests.Extensions;
using DiamondKata.DomainService.QueryHandlers;
using DiamondKata.DomainService.QueryHandlers.GetPadding;
using DiamondKata.DomainService.QueryHandlers.GetRowForChar;
using NUnit.Framework.Legacy;

namespace DiamondKata.Domain.UnitTests;

public class DiamondUnitTests
{
    [Test]
    public void DiamondHas2NMinus1NumberOfAscendingColumns() =>
        EnglishCharExtensions.GetAllEnglishCharacters()
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
        EnglishCharExtensions.GetAllEnglishCharacters()
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
        EnglishCharExtensions.GetAllEnglishCharacters()
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
        EnglishCharExtensions.GetAllEnglishCharactersExceptA()
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
        EnglishCharExtensions.GetAllEnglishCharactersExceptA()
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
        EnglishCharExtensions.GetAllEnglishCharactersExceptA().ToList()
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
        EnglishCharExtensions.GetAllEnglishCharactersExceptA().ToList()
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
        EnglishCharExtensions.GetAllEnglishCharactersExceptA().ToList()
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
        EnglishCharExtensions.GetAllEnglishCharactersExceptA().ToList()
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
                var leftHalf = line[..(midPoint + 1)];
                var rightHalf = line[midPoint..];

                CollectionAssert.AreEqual(leftHalf, rightHalf.Reverse());
            });
        });

    [Test]
    public void EnglishCharsAreInAscendingOrderFromTheTopHalfOfTheDiamond() =>
        EnglishCharExtensions.GetAllEnglishCharactersExceptA().ToList()
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
        EnglishCharExtensions.GetAllEnglishCharactersExceptA().ToList()
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

    private static IGetDiamondQueryHandler GetSystemUnderTest() => new GetDiamondQueryHandler(
        new GetRowForCharQueryHandler(new GetOuterPaddingQueryHandler(),
            new GetInnerPaddingQueryHandler()));
}