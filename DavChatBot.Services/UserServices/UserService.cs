using DatChatBot.DataLayer;
using DatChatBot.Services.DTOs;
using DatChatBot.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace DavChatBot.Services.UserServices
{
    public class UserService : BaseDbServices<User>, IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserService(DavChatBotDbContext dbContext, SignInManager<User> signInManager, UserManager<User> userManager) : base(dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> SignIn(SignInDTO user)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(user.DisplayName, user.Password, false, false);
            return signInResult.Succeeded;
        }

        public async Task<SignUpDTOResponse> SignUp(SignUpDTO user)
        {
            var response = new SignUpDTOResponse();

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.DisplayName))
            {
                response.Errors.Add("Invalid user.");
            }

            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.PasswordConfirm) || !user.Password.Equals(user.PasswordConfirm))
            {
                response.Errors.Add("Invalid user.");
                throw new ArgumentException("Invalid password.");
            }

            var newUser = new User
            {
                UserName = user.DisplayName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
            var identityResult = await _userManager.CreateAsync(newUser, user.Password);

            response.Success = identityResult.Succeeded;

            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    response.Errors.Add(item.Description);
                }
            }

            return response;
        }
    }
}

