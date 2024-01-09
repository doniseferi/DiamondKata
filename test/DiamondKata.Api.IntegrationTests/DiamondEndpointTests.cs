using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DiamondKata.Api.IntegrationTests;

public class DiamondEndpointTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    [Theory]
    [MemberData(nameof(GetAllEnglishChars))]
    public async Task ReturnsOkWhenCharIsAnEnglishLetter(char @char)
    {
        var response = await _httpClient.GetAsync($"/diamond/{@char}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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