using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using RatingAPI.Contract.Request;
using RatingAPI.Contract.Response;

namespace RatingsAPI.Tests.Integration;

public class RatingsControllerTests
{
    private readonly Host _applicationHost = new Host();
    private static readonly string ExistingTitleId = "a5777ae0-8272-42e3-aed4-be9ea6960b39"; 

    [Test]
    public async Task Should_ReturnRatings_When_ExistingTitleIdProvided()
    {
        var mockedHost = _applicationHost.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(svc =>
            {
                    // create mocks
            });
        });
        
        var getRequest = new GetRatingsRequest(ExistingTitleId);
        var response = await SendAsync<GetRatingsRequest, GetRatingsResponse>(
            HttpMethod.Get, "rating/get", getRequest);
        
        Assert.IsTrue(response.statusCode == 200);
        Assert.IsTrue(response.body.Ratings is not null);
        Assert.IsTrue(response.body.Ratings?.Any());
    }

    [Test]
    public async Task Should_ReturnNotFound_When_NonExistingTitleIdProvided()
    {
        var getRequest = new GetRatingsRequest(Guid.NewGuid().ToString());
        var response = await SendAsync<GetRatingsRequest, GetRatingsResponse>(
            HttpMethod.Get, "rating/get", getRequest);
        
        Assert.IsTrue(response.statusCode == 404);
        Assert.IsTrue(response.body.Ratings is null);
    }

    public Task Should_ReturnBadRequest_When_RestrictedContentProvided()
    {
        return Task.CompletedTask;
    }

    public Task Should_ReturnRatings_When_RatingForNewTitleIdAdded()
    {
        return Task.CompletedTask;
    }

    private async Task<(TResponse body, int statusCode)> SendAsync<TRequest, TResponse>(HttpMethod method, 
        string endpoint, TRequest request)
    {
        var httpClient = _applicationHost.CreateClient();

        var httpMessage = new HttpRequestMessage(method, endpoint);
        var jsonString = JsonSerializer.Serialize(request);

        httpMessage.Content = new StringContent(jsonString, new MediaTypeHeaderValue("application/json"));

        var httpResponse = await httpClient.SendAsync(httpMessage);

        TResponse responseBody;
        try
        {
            var stringContent = await httpResponse.Content.ReadAsStringAsync();
            responseBody = JsonSerializer.Deserialize<TResponse>(stringContent);
        }
        catch (Exception)
        {
            return (default, (int) httpResponse.StatusCode);
        }

        return (responseBody, (int) httpResponse.StatusCode);
    }
}