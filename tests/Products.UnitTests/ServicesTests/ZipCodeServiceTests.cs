using GatewayOcelot.Products.API.Constants;
using GatewayOcelot.Products.API.DataTransferObjects.Address;
using GatewayOcelot.Products.API.Interfaces.Mappers;
using GatewayOcelot.Products.API.Services;
using Moq;
using Moq.Protected;
using Products.UnitTests.TestBuilders;
using System.Net;
using System.Text.Json;

namespace Products.UnitTests.ServicesTests;

public sealed class ZipCodeServiceTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly Mock<IAddressMapper> _addressMapperMock;
    private readonly ZipCodeService _zipCodeService;

    public ZipCodeServiceTests()
    {
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _addressMapperMock = new Mock<IAddressMapper>();
        _zipCodeService = new ZipCodeService(
            _httpClientFactoryMock.Object,
            _addressMapperMock.Object);
    }

    [Fact]
    public async Task GetAddressByRequestAsync_SuccessfulScenario_ReturnsAddress()
    {
        // A
        var addressRequest = AddressBuilder.NewObject().RequestBuild();

        var addressApiResponse = AddressBuilder.NewObject().ApiResponseBuild();
        var addressApiResponseJsonString = JsonSerializer.Serialize(addressApiResponse);
        var httpResponseMessage = new HttpResponseMessage()
        {
            Content = new StringContent(addressApiResponseJsonString),
            StatusCode = HttpStatusCode.OK
        };
        const string baseAddress = "https://localhost";
        _httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
            ItExpr.Is<HttpRequestMessage>(h => h.RequestUri == new Uri($"{baseAddress}/ws/{addressRequest.ZipCode}/json/")),
            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri(baseAddress)
        };
        _httpClientFactoryMock.Setup(h => h.CreateClient(HttpClientNameConstants.ZipCodeHttpClient))
            .Returns(httpClient);

        var addres = AddressBuilder.NewObject().DomainBuild();
        _addressMapperMock.Setup(a => a.ApiResponseAndRequestToDomain(It.IsAny<AddressApiResponse>(), It.IsAny<AddressRequest>()))
            .Returns(addres);

        // A
        var addressResult = await _zipCodeService.GetAddressByRequestAsync(addressRequest);

        // A
        _addressMapperMock.Verify(a => a.ApiResponseAndRequestToDomain(It.IsAny<AddressApiResponse>(), It.IsAny<AddressRequest>()), Times.Once());

        Assert.NotNull(addressResult);
    }

    [Fact]
    public async Task GetAddressByRequestAsync_InvalidStatusCode_ReturnsNull()
    {
        // A
        var addressRequest = AddressBuilder.NewObject().RequestBuild();

        var httpResponseMessage = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.BadRequest
        };
        const string baseAddress = "https://localhost";
        _httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
            ItExpr.Is<HttpRequestMessage>(h => h.RequestUri == new Uri($"{baseAddress}/ws/{addressRequest.ZipCode}/json/")),
            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri(baseAddress)
        };
        _httpClientFactoryMock.Setup(h => h.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        // A
        var addressResult = await _zipCodeService.GetAddressByRequestAsync(addressRequest);

        // A
        _addressMapperMock.Verify(a => a.ApiResponseAndRequestToDomain(It.IsAny<AddressApiResponse>(), It.IsAny<AddressRequest>()), Times.Never());

        Assert.Null(addressResult);
    }

    [Fact]
    public async Task GetAddressByRequestAsync_InvalidResponse_ReturnsNull()
    {
        // A
        var addressRequest = AddressBuilder.NewObject().RequestBuild();

        var httpResponseMessage = new HttpResponseMessage()
        {
            Content = new StringContent("""{"erro": "true"}"""),
            StatusCode = HttpStatusCode.OK
        };
        const string baseAddress = "https://localhost";
        _httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
            ItExpr.Is<HttpRequestMessage>(h => h.RequestUri == new Uri($"{baseAddress}/ws/{addressRequest.ZipCode}/json/")),
            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri(baseAddress)
        };
        _httpClientFactoryMock.Setup(h => h.CreateClient(HttpClientNameConstants.ZipCodeHttpClient))
            .Returns(httpClient);

        var addres = AddressBuilder.NewObject().DomainBuild();
        _addressMapperMock.Setup(a => a.ApiResponseAndRequestToDomain(It.IsAny<AddressApiResponse>(), It.IsAny<AddressRequest>()))
            .Returns(addres);

        // A
        var addressResult = await _zipCodeService.GetAddressByRequestAsync(addressRequest);

        // A
        _addressMapperMock.Verify(a => a.ApiResponseAndRequestToDomain(It.IsAny<AddressApiResponse>(), It.IsAny<AddressRequest>()), Times.Never());

        Assert.Null(addressResult);
    }
}
