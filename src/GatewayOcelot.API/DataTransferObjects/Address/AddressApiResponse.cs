namespace GatewayOcelot.API.DataTransferObjects.Address;

public sealed record AddressApiResponse(
    string Logradouro,
    string Bairro,
    string Localidade,
    string Uf);
