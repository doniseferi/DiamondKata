using System.ComponentModel.DataAnnotations;
using DiamondKata.DomainService.Exception;
using DiamondKata.DomainService.QueryHandlers;
using DiamondKata.DomainService.ValueType;

namespace DiamondKata.Api.GetDiamondResultQueryHandler;

internal class GetDiamondResultQueryHandler(IGetDiamondQueryHandler getDiamondQueryHandler) : IGetDiamondResultQueryHandler
{
    public string Handle(char @char)
    {
        try
        {
            var englishChar = new EnglishChar(@char);
            var result = getDiamondQueryHandler.Handle(englishChar);
            return result;
        }
        catch (CharIsNotAnEnglishLetterException e)
        {
            throw new ValidationException(e.Message);
        }
    }
}