using DatChatBot.DataLayer.Entities;
using DatChatBot.Services.DTOs;

namespace DavChatBot.Services.UserServices
{
    public interface IUserService
    {
        Task<SignInDTOResponse> SignIn(SignInDTO user);
        Task<SignUpDTOResponse> SignUp(SignUpDTO user);
        Task<User?> GetUserAsync(int id);
        Task<User?> GetUserAsync(string userName);
    }
}

