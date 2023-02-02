using System.Text;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal class DiamondQueryHandler : IDiamondQueryHandler
{
    private readonly IRowGeneratorQueryHandler _rowGeneratorQueryHandler;

    public DiamondQueryHandler(IRowGeneratorQueryHandler rowGeneratorQueryHandler)
    {
        _rowGeneratorQueryHandler = rowGeneratorQueryHandler ??
                                    throw new ArgumentNullException(nameof(rowGeneratorQueryHandler));
    }

    public string Handle(EnglishChar @char)
    {
        if (@char == null)
            throw new ArgumentNullException(nameof(@char));

        var rows = GetAllRows(@char);

        return new StringBuilder()
            .Append(string
                .Join(
                    Environment.NewLine,
                    rows
                        .Select(x => x.Value)
                        .Reverse()))
            .Append(Environment.NewLine)
            .Append(string
                .Join(
                    Environment.NewLine,
                    rows
                        .Skip(1)
                        .Select(x => x.Value)))
            .ToString();
    }

    public Dictionary<EnglishChar, string> GetAllRows(EnglishChar @char)
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