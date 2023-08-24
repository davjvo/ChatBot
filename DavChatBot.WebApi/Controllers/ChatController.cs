using DavChatBot.Services.ChatServices;
using DavChatBot.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DavChatBot.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatController(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        [HttpGet]
        public IActionResult GetRecentMessages()
        {
            var recentMessages = _chatMessageService.GetRecentChatMessages();
            return new OkObjectResult(recentMessages);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage([FromBody] AddChatMessageDTO addChatMesageDto)
        {
            var response = await _chatMessageService.AddChatMessage(addChatMesageDto.Message, addChatMesageDto.UserId);
            return new OkObjectResult(response);
        }
    }
}

