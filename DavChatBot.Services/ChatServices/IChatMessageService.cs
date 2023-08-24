using DatChatBot.DataLayer.Entities;
using DavChatBot.Services.DTOs;

namespace DavChatBot.Services.ChatServices
{
    public interface IChatMessageService
    {
        Task<ChatMessage> AddChatMessage(string message, int userId);
        IEnumerable<ChatMessageDTO> GetRecentChatMessages();
    }
}

