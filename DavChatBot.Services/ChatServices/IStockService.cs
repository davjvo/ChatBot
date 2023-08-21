namespace DavChatBot.Services.ChatServices
{
    public interface IStockService
    {
        Task<string> GetStockMessage(string baseUrl, string parameter, CancellationToken ct);
    }
}

