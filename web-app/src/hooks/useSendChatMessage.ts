import { useSignalR } from "../context/SignalRContext";

const useSendChatMessage = () => {
  const hubConnection = useSignalR();

  const sendChatMessage = async (username: string, message: string, userId: number) => {
    if (!hubConnection) {
      console.error("No SignalR connection found.");
      return;
    }

    try {
      await hubConnection.invoke("SendMessage", username, message, userId);
    } catch (err) {
      console.error("Error sending message via SignalR", err);
    }
  };

  return sendChatMessage;
};

export default useSendChatMessage;
