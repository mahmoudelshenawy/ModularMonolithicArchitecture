using Module.Users.Core.Dtos;

namespace Module.Users.Core.Interfaces
{
    public interface IUserAuthenticationRepository
    {
        Task<RegisterDto> RegisterUser(RegisterDto registerDto);
        Task<RegisterDto> ConfirmUserEmail(string userId, string token);
        Task<ForgotPasswordDto> SendResetPasswordConfirmationToUser(ForgotPasswordDto forgotPassword);
        Task<ResetPasswordDto> ResetPassword(ResetPasswordDto resetPasswordDto);

        Task<AuthModelDto> GetTokenAsync(LoginDto model);
        Task<AuthModelDto> GenerateRegreshToken(RefreshTokenDto refreshTokenDto);
        Task<bool> RevokeToken(string userId);
    }
}
