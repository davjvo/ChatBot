using Microsoft.AspNetCore.Identity;

namespace DatChatBot.DataLayer.Entities
{
    public class User : IdentityUser<int>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}