using DiamondKata.Api;
using DiamondKata.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddGetDiamondQueryHandler();
var app = builder.Build();

app.MapGet("/diamond/{char}", GetDiamond)
    .AddEndpointFilter<EnglishCharValidationFilter>();

app.Run();

static IResult GetDiamond(char @char) => TypedResults.Ok(@char);

public partial class Program
{ }