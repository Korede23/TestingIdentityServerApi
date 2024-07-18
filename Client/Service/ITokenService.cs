using IdentityModel.Client;

namespace Client.Service
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
