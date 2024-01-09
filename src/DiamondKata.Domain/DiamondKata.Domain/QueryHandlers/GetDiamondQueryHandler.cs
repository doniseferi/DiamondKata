using System.Text;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal class GetDiamondQueryHandler(IRowGeneratorQueryHandler rowGeneratorQueryHandler) : IDiamondQueryHandler
{
    private readonly IRowGeneratorQueryHandler _rowGeneratorQueryHandler =
        rowGeneratorQueryHandler ?? throw new ArgumentNullException(nameof(rowGeneratorQueryHandler));

    public string Handle(EnglishChar @char)
    {
        ArgumentNullException.ThrowIfNull(@char);

        var rows = GenerateDiamondRows(@char);

        var topHalfOfDiamond = AppendTopHalfOfDiamond();

        var lowerHalfOfDiamond = AppendBottomHalfOfDiamond();

        return new StringBuilder()
            .Append(topHalfOfDiamond)
            .Append(Environment.NewLine)
            .Append(lowerHalfOfDiamond)
            .ToString();

        string AppendTopHalfOfDiamond() => string
                .Join(
                    Environment.NewLine,
                    rows
                        .Select(x => x.Value)
                        .Reverse());

        string AppendBottomHalfOfDiamond() => string
                .Join(
                    Environment.NewLine,
                    rows
                        .Skip(1)
                        .Select(x => x.Value));
    }

    private Dictionary<EnglishChar, string> GenerateDiamondRows(EnglishChar @char)
    {
        const int asciiValueForUpperCaseA = 65;

        var rows = new Dictionary<EnglishChar, string>();

        for (var i = (int)@char.Value; i > asciiValueForUpperCaseA - 1; i--)
        {
            var englishChar = new EnglishChar((char)i);
            var row = _rowGeneratorQueryHandler.Handle(englishChar, @char);
            rows.Add(englishChar, row);
        }

        return rows;
    }
}