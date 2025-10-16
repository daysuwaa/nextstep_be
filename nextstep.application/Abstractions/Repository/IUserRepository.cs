using nextstep.application.DTOs.Responses;
using nextstep.domain.Entities;

namespace nextstep.application.InterFaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User? GetSingleUser(string email, string password);
    }
}