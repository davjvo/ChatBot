using DatChatBot.DataLayer.Records;

namespace DavChatBot.Services.ChatServices
{
    public class BotCommandService : IBotCommandService
    {
        public readonly IReadOnlyList<string> _availableCommands =
            new List<string> {
            "/stock"
        };

        private const string DefaultErrorMessage = "Error found in command execution";

        public CommandInformation GetCommandInformation(string text)
        {
            var error = ValidateCommand(text);

            if (!string.IsNullOrWhiteSpace(error))
            {
                var errorMessage = new CommandInformation(string.Empty, string.Empty, error);

                return errorMessage;
            }

            var commandInfo = ConvertToCommandInformation(text);

            commandInfo ??= new CommandInformation(string.Empty, string.Empty, DefaultErrorMessage);

            return commandInfo;
        }

        public string? ValidateCommand(string text)
        {
            var commandInfo = ConvertToCommandInformation(text);

            if (commandInfo == null)
            {
                if (!text.StartsWith("/"))
                    return "Invalid command format, please use '/' to use this command.";


                if (!text.Contains('='))
                    return "Invalid parameter provided, please add '=' to specify the command";

                return DefaultErrorMessage;
            }

            if (!_availableCommands.Contains(commandInfo.Command))
                return $"'{commandInfo.Command}' Command not found.";

            return string.IsNullOrWhiteSpace(commandInfo.Parameter)
                ? "Paramater can not be null"
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