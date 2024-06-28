using GatewayOcelot.Products.API.Constants;
using GatewayOcelot.Products.API.DataTransferObjects.Address;
using GatewayOcelot.Products.API.Entities;
using GatewayOcelot.Products.API.Interfaces.Mappers;
using GatewayOcelot.Products.API.Interfaces.Services;
using System.Text.Json;

namespace GatewayOcelot.Products.API.Services;

public sealed class ZipCodeService(
    IHttpClientFactory httpClientFactory,
    IAddressMapper addressMapper)
    : IZipCodeService
{
    public async Task<Address?> GetAddressByRequestAsync(AddressRequest addressRequest)
    {
        using var httpClient = httpClientFactory.CreateClient(HttpClientNameConstants.ZipCodeHttpClient);

        var requestUri = $"/ws/{addressRequest.ZipCode}/json/";

        var getZipCodeHttpResponseMessage = await httpClient.GetAsync(requestUri);
        const string errorJsonString = """"erro": "true"""";

        if (!getZipCodeHttpResponseMessage.IsSuccessStatusCode)
        {
            return null;
        }

        var apiResponseJsonString = await getZipCodeHttpResponseMessage.Content.ReadAsStringAsync();

        if (apiResponseJsonString.Contains(errorJsonString))
        {
            return null;
        }

        var jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        var addressApiResponse = JsonSerializer.Deserialize<AddressApiResponse>(apiResponseJsonString, jsonOptions);

        return addressMapper.ApiResponseAndRequestToDomain(addressApiResponse!, addressRequest);
    }
}
