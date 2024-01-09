using System.ComponentModel.DataAnnotations;
using DiamondKata.Api;
using DiamondKata.Api.Extensions;
using DiamondKata.Api.GetDiamondResultQueryHandler;
using DiamondKata.DomainService.Exception;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddGetDiamondQueryHandler();
var app = builder.Build();

app.MapGet("/diamond/{char}", GetDiamond)
    .AddEndpointFilter<EnglishCharValidationFilter>();

app.Run();

static IResult GetDiamond(char @char, IGetDiamondResultQueryHandler getDiamondResultQueryHandler)
{
    try
    {
        return TypedResults.Ok(getDiamondResultQueryHandler.Handle(@char));
    }
    catch (ValidationException e)
    {
        return TypedResults.BadRequest(e.Message);
    }
    catch (CharIsNotAnEnglishLetterException e)
    {
        return TypedResults.BadRequest(e.Message);
    }
}

namespace DiamondKata.Api
{
    public partial class Program
    {
    }
}