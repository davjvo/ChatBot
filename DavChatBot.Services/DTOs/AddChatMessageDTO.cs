namespace DavChatBot.Services.DTOs
{
    public class AddChatMessageDTO
    {
        public string Message { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}

