using System.Text;
using System.Text.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

[TestCaseOrderer("HomeEnergyApi.Tests.Extensions.PriorityOrderer", "HomeEnergyApi.Tests")]
public class Test
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private string testHome = JsonSerializer.Serialize(new Home(1, "Test", "123 Test St.", "Test City", 123));
    private string putTestHome = JsonSerializer.Serialize(new Home(2, "PUT", "I was put here", "So your PUT works!", 456));
    private string expectedNotFound = JsonSerializer.Serialize(new Home(0, "Nobody", "000 Nowhere Ave", "No Owner Was Found", 0));

    public Test(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory, TestPriority(1)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsSuccessfulHTTPResponseCodeOnGETHomes(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.True(response.IsSuccessStatusCode, $"HomeEnergyApi did not return successful HTTP Response Code on GET request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");
    }

    [Theory, TestPriority(2)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsSuccessfulHTTPResponseOnAddingHomeThroughPOST(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Post, url);
        sendRequest.Content = new StringContent(testHome,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        Assert.True(response.IsSuccessStatusCode, $"HomeEnergyApi did not return successful HTTP Response Code on POST request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

        string responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(responseContent == testHome, $"HomeEnergyApi did not return the Home being added as a response from the POST request at {url}; \n Expected : {testHome} \n Received : {responseContent} \n");
    }

    [Theory, TestPriority(3)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsHomeAddedFromPOSTOnGET(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.True(response.IsSuccessStatusCode, $"HomeEnergyApi did not return successful HTTP Response Code on GET request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

        string responseContent = await response.Content.ReadAsStringAsync();
        string expectedContent = $"[{testHome}]";
        Assert.True(responseContent == expectedContent, $"HomeEnergyApi did not return the correct Home on GET request after making POST at {url}; \n Expected : {expectedContent} \n Received : {responseContent} \n");
    }

    [Theory, TestPriority(4)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsSuccessfulHTTPResponseOnPUT(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Put, url + "/Test");
        sendRequest.Content = new StringContent(putTestHome,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        Assert.True(response.IsSuccessStatusCode, $"HomeEnergyApi did not return successful HTTP Response Code on PUT request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

        string responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(responseContent == putTestHome, $"HomeEnergyApi did not return the home being added as a response from the PUT request at {url}; \n Expected : {putTestHome} \n Received : {responseContent} \n");
    }

    [Theory, TestPriority(5)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsCorrectHomeOnGETAfterPUT(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.True(response.IsSuccessStatusCode, $"HomeEnergyApi did not return successful HTTP Response Code on GET request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

        string responseContent = await response.Content.ReadAsStringAsync();
        string expectedContent = $"[{putTestHome}]";
        Assert.True(responseContent == expectedContent, $"HomeEnergyApi did not return the correct Home on GET request after making PUT at {url}; \n Expected : {expectedContent} \n Received : {responseContent} \n");
    }

    [Theory, TestPriority(6)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsNoContetHTTPResponseIfTryingToPUTHomeThatDoesntExist(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Put, url + "/Nobody");
        sendRequest.Content = new StringContent(putTestHome,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        Assert.True((int)response.StatusCode == 204, $"HomeEnergyApi did not return HTTP Response \"204: NoContent\" on PUT request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");
    }
}
