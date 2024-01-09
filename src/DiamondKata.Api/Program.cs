using DiamondKata.Api;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/diamond/{char}", GetDiamond)
    .AddEndpointFilter<EnglishCharValidationFilter>();

app.Run();

static IResult GetDiamond(char @char) => TypedResults.Ok(@char);

public partial class Program
{ }