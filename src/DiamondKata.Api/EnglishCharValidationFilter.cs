namespace DiamondKata.Api;

public class EnglishCharValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var @char = char.ToUpperInvariant(context.GetArgument<char>(0));

        if (!IsAnEnglishLetter(@char))
        {
            return Results.BadRequest("Only english characters are allowed");
        }

        return await next(context);

        bool IsAnEnglishLetter(char c) =>
            Enumerable.Range('A', 26)
                .Select(x => (char)x)
                .Contains(c);
    }
}