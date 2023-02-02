﻿using DiamondKata.DomainService.ValueType;

namespace DiamondKata.DomainService.QueryHandlers;

interface IRowGeneratorQueryHandler
{
    string Handle(EnglishChar @char, EnglishChar lastCharInDiamond);
}