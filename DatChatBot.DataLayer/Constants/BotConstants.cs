namespace DatChatBot.DataLayer.Constants
{
    public static class BotConstants
    {
        public const int Id = 10000;
        public const string Name = "Bot";
        public const string Mail = $"{Name}@example.com";

        public static class Errors
        {
            public const string Defaul = "Error found in command execution";
            public const string InvalidParameter = "Invalid parameter provided, please add '=' to specify the command";
            public const string InvalidCommandFormat = "Invalid command format, please use '/' to use this command.";
            public const string NullParameter = "Paramater can not be null";

        }
    }

    public static class BotCommands
    {
        public const string StockCommand = "/stock";
    }
}

