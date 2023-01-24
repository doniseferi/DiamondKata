using System.Text;
using DiamondKata.Domain.ValueType;
using DiamondKata.DomainService.QueryHandlers;

namespace DiamondKata.DomainService.Generators;

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