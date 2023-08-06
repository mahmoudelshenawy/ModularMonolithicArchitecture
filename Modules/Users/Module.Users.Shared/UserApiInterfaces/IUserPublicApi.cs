using Module.Users.Shared.Dtos;
using Shared.Models.Models;

namespace Module.Users.Shared.UserApiInterfaces
{
    public interface IUserPublicApi
    {
        Task<IBaseUser> GetUserDetails(string userId);
        Task<bool> EmailIsUnique(string email);

        Task<string> GetUserEmailById(string userId);
        Task<Result<string>> RegisterNewUser(string email, string name, string phone, string password, string[] roles);

        Task<bool> UpdateUserCredentials(string userId, string email, string password, string? phone);
        Task<bool> UpdateUserRoles(string userId, string[] roles);
    }
}
