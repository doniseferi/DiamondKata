using System.Net;
using System.Text.RegularExpressions;
using DiamondKata.DomainService.QueryHandlers;
using DiamondKata.DomainService.ValueType;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DiamondKata.Api.IntegrationTests;

public class DiamondEndpointTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient = factory.CreateClient();
    private readonly IGetDiamondQueryHandler _getDiamondQueryHandler = factory.Services.GetRequiredService<IGetDiamondQueryHandler>();

    [Theory]
    [MemberData(nameof(GetAllEnglishChars))]
    public async Task ReturnsOkWhenCharIsAnEnglishLetter(char @char)
    {
        var response = await _httpClient.GetAsync($"/diamond/{@char}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(GetAllEnglishChars))]
    public async Task ReturnsCorrectDiamondPatternWhenCharIsAnEnglishLetter(char @char)
    {
        var response = await _httpClient.GetAsync($"/diamond/{@char}");
        response.EnsureSuccessStatusCode();

        var content = JsonConvert.DeserializeObject<string>(
            await response.Content.ReadAsStringAsync());
        var expectedPattern = _getDiamondQueryHandler.Handle(new EnglishChar(@char));
        Assert.Equal(expectedPattern, content);
    }

    [Fact]
    public async Task ReturnsBadRequestWhenCharIsNotAnEnglishLetter()
    {
        var response = await _httpClient.GetAsync($"/diamond/1");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public static IEnumerable<object[]> GetAllEnglishChars()
    {
        for (var @char = 'a'; @char <= 'z'; @char++)
        {
            yield return [@char];
        }
    }
}