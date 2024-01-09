var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/diamond/{char}", GetDiamond)
.AddEndpointFilter(async (context, next) =>
{
    var @char = char.ToUpperInvariant(context.GetArgument<char>(0));

    bool IsAnEnglishLetter(char c) =>
        Enumerable.Range('A', 26)
            .Select(x => (char)x)
            .Contains(c);

    if (!IsAnEnglishLetter(@char))
    {
        return Results.BadRequest("Only english characters are allowed");
    }

    return await next(context);
});

app.Run();

static IResult GetDiamond(char @char) => TypedResults.Ok(@char);

public partial class Program
{ }