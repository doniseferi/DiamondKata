using DiamondKata.Domain.ValueType;
using System.Text;

namespace DiamondKata.Domain.Builders;

internal interface IDiamondQueryHandler
{
    string Handle(DiamondRequest request);
}

internal class DiamondQueryHandler : IDiamondQueryHandler
{
    private readonly IRowGeneratorQueryHandler _rowGeneratorQueryHandler;
    private readonly IGetLowerEnglishLettersQueryHandlers _getLowerEnglishLettersQueryHandlers;

    public DiamondQueryHandler(IRowGeneratorQueryHandler rowGeneratorQueryHandler,
        IGetLowerEnglishLettersQueryHandlers getLowerEnglishLettersQueryHandlers)
    {
        _rowGeneratorQueryHandler = rowGeneratorQueryHandler ??
                                    throw new ArgumentNullException(nameof(rowGeneratorQueryHandler));

        _getLowerEnglishLettersQueryHandlers = getLowerEnglishLettersQueryHandlers ??
                                               throw new ArgumentNullException(
                                                   nameof(getLowerEnglishLettersQueryHandlers));
    }

    public string Handle(DiamondRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));


        var rows = GenerateOutputPerChar(request);

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


    private Dictionary<EnglishChar, string> GenerateOutputPerChar(DiamondRequest request)
        => _getLowerEnglishLettersQueryHandlers
            .Handle(request.RequestChar)
            .Select(currentChar => new
            {
                Key = currentChar,
                Value = _rowGeneratorQueryHandler.Handle(
                    new RowQueryRequest(
                        request.RequestChar,
                        currentChar,
                        request.OuterPaddingChar,
                        request.InnerPaddingChar))
            })
            .ToDictionary(t => t.Key, t => t.Value);
}

internal interface IGetLowerEnglishLettersQueryHandlers
{
    IReadOnlyCollection<EnglishChar> Handle(EnglishChar @char);
}

internal class GetLowerEnglishLettersQueryHandlers : IGetLowerEnglishLettersQueryHandlers
{
    private const int AsciiValueForUpperCaseA = 65;

    public IReadOnlyCollection<EnglishChar> Handle(EnglishChar @char)
    {
        if (@char == null)
            throw new ArgumentNullException(nameof(@char));

        var englishLetterAccum = new List<EnglishChar>() {@char};

        for (var i = @char.GetNumericalValue(); i > 0; i--)
        {
            var previousEnglishChar = GetCharPriorTo(i);
            englishLetterAccum.Add(previousEnglishChar);
        }

        return englishLetterAccum;
    }

    private EnglishChar GetCharPriorTo(int currentCharsNumericalValue)
    {
        var previousCharsAsciiValue = (AsciiValueForUpperCaseA + (currentCharsNumericalValue - 1));
        return new EnglishChar((char) previousCharsAsciiValue);
    }
}

class RowQueryRequest
{
    public RowQueryRequest(
        EnglishChar inputChar,
        EnglishChar currentRowsChar,
        PaddingChar outerPaddingChar,
        PaddingChar innerPaddingChar)
    {
        RequestChar = inputChar ?? throw new ArgumentNullException(nameof(inputChar));
        CurrentRowsChar = currentRowsChar ?? throw new ArgumentNullException(nameof(currentRowsChar));
        OuterPaddingChar = outerPaddingChar ?? throw new ArgumentNullException(nameof(outerPaddingChar));
        InnerPaddingChar = innerPaddingChar ?? throw new ArgumentNullException(nameof(innerPaddingChar));
    }

    public EnglishChar RequestChar { get; }
    public EnglishChar CurrentRowsChar { get; }
    public PaddingChar OuterPaddingChar { get; }
    public PaddingChar InnerPaddingChar { get; }
}

interface IRowGeneratorQueryHandler
{
    string Handle(RowQueryRequest request);
}

class DiamondRequest
{
    public DiamondRequest(
        EnglishChar inputChar,
        PaddingChar outerPaddingChar,
        PaddingChar innerPaddingChar)
    {
        RequestChar = inputChar ?? throw new ArgumentNullException(nameof(inputChar));
        OuterPaddingChar = outerPaddingChar ?? throw new ArgumentNullException(nameof(outerPaddingChar));
        InnerPaddingChar = innerPaddingChar ?? throw new ArgumentNullException(nameof(innerPaddingChar));
    }

    public EnglishChar RequestChar { get; }
    public PaddingChar OuterPaddingChar { get; }
    public PaddingChar InnerPaddingChar { get; }
}

internal class RowGeneratorQueryHandler : IRowGeneratorQueryHandler
{
    private readonly IOuterPaddingStringGenerator _outerPaddingStringGenerator;
    private readonly IInnerPaddingStringGenerator _innerPaddingStringGenerator;


    public RowGeneratorQueryHandler(IOuterPaddingStringGenerator outerPaddingStringGenerator,
        IInnerPaddingStringGenerator innerPaddingStringGenerator)
    {
        _outerPaddingStringGenerator = outerPaddingStringGenerator ??
                                       throw new ArgumentNullException(nameof(outerPaddingStringGenerator));
        _innerPaddingStringGenerator = innerPaddingStringGenerator ??
                                       throw new ArgumentNullException(nameof(innerPaddingStringGenerator));
    }

    public string Handle(RowQueryRequest request)
    {
        var outerPadding =
            _outerPaddingStringGenerator.Generate(request.RequestChar, request.CurrentRowsChar,
                request.OuterPaddingChar);

        var innerPadding =
            _innerPaddingStringGenerator.Generate(request.CurrentRowsChar, request.InnerPaddingChar);

        var currentChar = request.CurrentRowsChar;

        var sb = new StringBuilder();
        sb.Append(outerPadding);
        sb.Append(currentChar.Value);
        sb.Append(innerPadding);
        return currentChar.Value == char.ToUpperInvariant('A')
            ? sb.Append(outerPadding).ToString()
            : sb.Append(currentChar.Value).Append(outerPadding).ToString();
    }
}

internal interface IInnerPaddingLengthQueryHandler
{
    int Handle(EnglishChar @char);
}

internal interface IInnerPaddingStringGenerator
{
    string Generate(EnglishChar @char, PaddingChar innerPaddingChar);
}

internal class InnerPaddingStringGenerator : IInnerPaddingStringGenerator
{
    private readonly IInnerPaddingLengthQueryHandler _innerPaddingLengthQueryHandler;

    public InnerPaddingStringGenerator(IInnerPaddingLengthQueryHandler innerPaddingLengthQueryHandler)
    {
        _innerPaddingLengthQueryHandler = innerPaddingLengthQueryHandler ??
                                          throw new ArgumentNullException(nameof(innerPaddingLengthQueryHandler));
    }

    public string Generate(EnglishChar @char, PaddingChar innerPaddingChar)
    {
        if (@char == null)
            throw new ArgumentNullException(nameof(@char));

        if (innerPaddingChar == null)
            throw new ArgumentNullException(nameof(innerPaddingChar));

        var sb = new StringBuilder();

        var innerPaddingCharLength = _innerPaddingLengthQueryHandler.Handle(@char);

        for (var i = 0; i < innerPaddingCharLength; i++)
            sb.Append(innerPaddingChar.Value);

        return sb.ToString();
    }
}

internal interface IOuterPaddingStringGenerator
{
    string Generate(EnglishChar requestChar, EnglishChar currentRowsChar, PaddingChar outerPaddingChar);
}

internal class OuterPaddingStringGenerator : IOuterPaddingStringGenerator
{
    private readonly IOuterPaddingLengthQueryHandler _outerPaddingLengthQueryHandler;

    public OuterPaddingStringGenerator(IOuterPaddingLengthQueryHandler outerPaddingLengthQueryHandler)
    {
        _outerPaddingLengthQueryHandler = outerPaddingLengthQueryHandler ??
                                          throw new ArgumentNullException(nameof(outerPaddingLengthQueryHandler));
    }

    public string Generate(EnglishChar requestChar, EnglishChar currentRowsChar, PaddingChar outerPaddingChar)
    {
        if (requestChar == null)
            throw new ArgumentNullException(nameof(requestChar));

        if (currentRowsChar == null)
            throw new ArgumentNullException(nameof(currentRowsChar));

        if (outerPaddingChar == null)
            throw new ArgumentNullException(nameof(outerPaddingChar));

        var sb = new StringBuilder();

        var outerPaddingCharLength = _outerPaddingLengthQueryHandler.Handle(requestChar, currentRowsChar);

        for (var i = 0; i < outerPaddingCharLength; i++)
            sb.Append(outerPaddingChar.Value);

        return sb.ToString();
    }
}

internal interface IOuterPaddingLengthQueryHandler
{
    int Handle(EnglishChar requestChar, EnglishChar @char);
}

internal class OuterPaddingLengthQueryHandler : IOuterPaddingLengthQueryHandler
{
    public int Handle(EnglishChar requestChar, EnglishChar @char)
    {
        var requestCharValue = requestChar.GetNumericalValue() * 2 + 1;
        var charValue = @char.GetNumericalValue() * 2 + 1;
        return (requestCharValue - charValue) / 2;
    }
}

internal class InnerPaddingLengthQueryHandler : IInnerPaddingLengthQueryHandler
{
    public int Handle(EnglishChar @char)
    {
        var charsNumericalValue = @char.GetNumericalValue();

        var numberOfCharsNeededForInternalPadding = (charsNumericalValue * 2) - 1;

        return numberOfCharsNeededForInternalPadding < 0
            ? 0
            : numberOfCharsNeededForInternalPadding;
    }
}