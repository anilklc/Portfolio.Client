using PortfolioClient.DTO.Login;
using PortfolioClient.DTO.Token;

namespace PortfolioClient.Service.Interfaces
{
    public interface IAuthService
    {
        Task<HttpResponseMessage> AuthenticateAsync(string endpoint, LoginDTO login);
        void SetTokenInCookie(Token token);
        void RemoveTokenFromCookie();
        Task<bool> HasTokenInCookie();
    }
}
