import { RecentMessagesDto } from "../DTOs/GetRecentMessagesDTO";
import { Constants } from "../utils/constants";

export const MessageService = {
  getRecentMessages: async (): Promise<RecentMessagesDto[]> => {
    const response = await fetch(`${Constants.API_BASE_URL}Chat/GetRecentMessages`, {
      method: 'Get',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
    });

    const data = await response.json();
    return data;
  },
}