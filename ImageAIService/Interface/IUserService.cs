using ImageAIService.DTO.UsereServcieDTO;
using ImageAIService.Models;

namespace ImageAIService.Interface
{
    public interface IUserService
    {
        Task<UserResponseDto>  CreateUserAsync (CreateUserDto user);
        Task<User?> GetUserByIdAsync (int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync (int id);
        Task<bool> UpdateUserAsync (User user);
        Task<bool> ResetPersonPassword(ResetUserPasswordInputDTO input);
        Task<string> SignIn(SignInInputDTO input);

    }
}
