namespace DatChatBot.Services.DTOs
{
    public class SignInDTO
    {
        public string DisplayName { get; set; }
        public string Password { get; set; }
    }

    public class SignInDTOResponse
    {
        public bool Success { get; set; }
        public int UserId { get; set; }
        public List<string> Errors { get; set; }

        public SignInDTOResponse()
        {
            Errors = new List<string>();
        }
    }
}

