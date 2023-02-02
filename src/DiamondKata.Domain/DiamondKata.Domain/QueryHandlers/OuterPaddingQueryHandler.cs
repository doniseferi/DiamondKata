﻿using System.Text;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

internal class OuterPaddingQueryHandler : IOuterPaddingQueryHandler
{
    private const char OuterPaddingChar = ' ';

    public string Handle(EnglishChar @char, EnglishChar lastCharInDiamond)
    {
        if (@char == null)
            throw new ArgumentNullException(nameof(@char));

        if (lastCharInDiamond == null)
            throw new ArgumentNullException(nameof(lastCharInDiamond));

        var resultBuilder = new StringBuilder();

        var outerPaddingCharLength = CalculateOuterPadding(@char, lastCharInDiamond);

        for (var i = 0; i < outerPaddingCharLength; i++)
            resultBuilder.Append(OuterPaddingChar);

        return resultBuilder.ToString();
    }

    private static int CalculateOuterPadding(EnglishChar @char, EnglishChar lastChar)
    {
        var lastCharsOrderValue = lastChar.GetOrderInAlphabet() * 2 + 1;

        var charsOrderValue = @char.GetOrderInAlphabet() * 2 + 1;

        return (lastCharsOrderValue - charsOrderValue) / 2;
    }
}