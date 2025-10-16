using nextstep.application.DTOs.Responses;
using nextstep.domain.Entities;

namespace nextstep.application.Abstractions.Core
{
    public interface IUserHandler
    {
        Task<User> RegisterAsync(string username, string email, string password, CancellationToken cancellationToken = default);
        Task<TokenResponse> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    }
}