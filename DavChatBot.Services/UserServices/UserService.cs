using DatChatBot.DataLayer;
using DatChatBot.Services.DTOs;
using DatChatBot.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DavChatBot.Services.UserServices
{
    public class UserService : BaseDbServices<User>, IUserService
    {
        private readonly SignInManager<User>? _signInManager;
        private readonly UserManager<User>? _userManager;

        public UserService(IDbContextFactory<DavChatBotDbContext> dbContextFactory, SignInManager<User>? signInManager = null, UserManager<User>? userManager = null) : base(dbContextFactory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<SignInDTOResponse> SignIn(SignInDTO user)
        {
            if (_signInManager == null)
                throw new ArgumentException("Identity not configured");

            var response = new SignInDTOResponse();
            var signInResult = await _signInManager.PasswordSignInAsync(user.DisplayName, user.Password, false, false);

            if (!signInResult.Succeeded)
                return response;

            var dbUser = await GetUserAsync(user.DisplayName);
            response.UserId = dbUser!.Id;
            response.Success = true;
            return response;
        }

        public async Task<SignUpDTOResponse> SignUp(SignUpDTO user)
        {
            if (_userManager == null)
                throw new ArgumentException("Identity not configured");

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

        public async Task<User?> GetUserAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<User?> GetUserAsync(string userName)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(m => userName.Equals(m.UserName));
        }
    }
}

