import { PostMessageDTO } from "../DTOs/PostMessageDTO";
import { Constants } from "../utils/constants";

export const MessageService = {
  postMessage: async (message: PostMessageDTO) => {
    const response = await fetch(`${Constants.API_BASE_URL}Message/Post`, {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(message),
    });

    if (!response.ok) {
      throw new Error('Sign In failed');
    }

    const data = await response.json();
    return data;
  },
}