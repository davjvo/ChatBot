using DatChatBot.DataLayer.Constants;
using DatChatBot.DataLayer.Records;

namespace DavChatBot.Services.ChatServices
{
    public class BotCommandService : IBotCommandService
    {
        public readonly IReadOnlyList<string> _availableCommands;

        public BotCommandService()
        {
            _availableCommands = new List<string> {
                BotCommands.StockCommand
            };
        }

        public CommandInformation GetCommandInformation(string text)
        {
            var error = ValidateCommand(text);

            if (!string.IsNullOrWhiteSpace(error))
            {
                var errorMessage = new CommandInformation(string.Empty, string.Empty, error);

                return errorMessage;
            }

            var commandInfo = ConvertToCommandInformation(text);

            commandInfo ??= new CommandInformation(string.Empty, string.Empty, BotConstants.Errors.Defaul);

            return commandInfo;
        }

        public string? ValidateCommand(string text)
        {
            var commandInfo = ConvertToCommandInformation(text);

            if (commandInfo == null)
            {
                if (!text.StartsWith("/"))
                    return BotConstants.Errors.InvalidCommandFormat;


                if (!text.Contains('='))
                    return BotConstants.Errors.InvalidParameter;

                return BotConstants.Errors.Defaul;
            }

            if (!_availableCommands.Contains(commandInfo.Command))
                return $"'{commandInfo.Command}' Command not found.";

            return string.IsNullOrWhiteSpace(commandInfo.Parameter)
                ? BotConstants.Errors.NullParameter
                : null;
        }

        public bool IsCommand(string text) => text.StartsWith("/", StringComparison.InvariantCultureIgnoreCase);

        private CommandInformation? ConvertToCommandInformation(string text)
        {
            if (!text.StartsWith("/"))
                return null;

            if (!text.Contains('='))
                return null;

            var splitter = text.Split("=");
            var (command, parameter) = (splitter[0].Trim(), splitter[1].Trim());

            var commandInfo = new CommandInformation(command, parameter);

            return commandInfo;
        }
    }
}