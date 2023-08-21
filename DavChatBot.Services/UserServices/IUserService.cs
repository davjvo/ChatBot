using DatChatBot.Services.DTOs;

namespace DavChatBot.Services.UserServices
{
    public interface IUserService
    {
        Task<bool> SignIn(SignInDTO user);
        Task<SignUpDTOResponse> SignUp(SignUpDTO user);
    }
}

