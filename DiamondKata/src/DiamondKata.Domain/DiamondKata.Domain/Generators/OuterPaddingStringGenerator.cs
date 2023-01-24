﻿using System.Text;
using DiamondKata.Domain.ValueType;
using DiamondKata.DomainService.QueryHandlers;

namespace DiamondKata.DomainService.Generators;

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

        var outerPaddingCharLength =
            _outerPaddingLengthQueryHandler
                .Handle(requestChar, currentRowsChar);

        for (var i = 0; i < outerPaddingCharLength; i++)
            sb.Append(outerPaddingChar.Value);

        return sb.ToString();
    }
}