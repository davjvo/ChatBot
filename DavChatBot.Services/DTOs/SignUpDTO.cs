namespace DatChatBot.Services.DTOs
{
    public class SignUpDTO
    {
        public string DisplayName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordConfirm { get; set; } = string.Empty;
    }

    public class SignUpDTOResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }

        public SignUpDTOResponse()
        {
            Errors = new List<string>();
        }
    }
}

