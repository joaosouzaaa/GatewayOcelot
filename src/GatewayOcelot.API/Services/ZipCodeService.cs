using GatewayOcelot.API.Constants;
using GatewayOcelot.API.DataTransferObjects.Address;
using GatewayOcelot.API.DataTransferObjects.Error;
using GatewayOcelot.API.Entities;
using GatewayOcelot.API.Interfaces.Mappers;
using GatewayOcelot.API.Interfaces.Services;
using System.Text.Json;

namespace GatewayOcelot.API.Services;

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
