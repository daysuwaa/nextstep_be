using nextstep.application.DTOs.Responses;
using nextstep.domain.Entities;

namespace nextstep.application.Abstractions.Service
{
	public interface ITokenService
	{
        //string TokenService();
        //Token GenerateToken(string email);
        Token GenerateToken(User user);
    }
}
