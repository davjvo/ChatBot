import Container from "@mui/material/Container";
import CssBaseline from "@mui/material/CssBaseline";
import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";
import React, { useEffect, useState } from "react";
import Button from "@mui/material/Button";
import { useLocation } from "react-router-dom";
import useSendChatMessage from "../hooks/useSendChatMessage";
import LatestMessages, { Message } from "../components/LatestMessages";
import { useSignalR } from "../context/SignalRContext";
import { MessageService } from "../services/chatService";

export const Chat: React.FC = () => {
  const [message, setMessage] = useState('');
  const [recentMessages, setRecentMessages] = useState<Message[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const sendMessage = useSendChatMessage();
  const hubConnection = useSignalR();
  const { state } = useLocation();
  const { user, userId } = state;

  const handleSendMessage = (_: React.MouseEvent<HTMLElement>) => {
    sendMessage(user, message, userId);
    setMessage("");
  }

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setMessage(event.target.value);
  }
  useEffect(() => {
    const fetchRecentMessages = async () => {
      const messages = await MessageService.getRecentMessages();
      setRecentMessages(messages.map(current => {
        var recentMessage: Message = {
          user: current.userName,
          content: current.message
        };
        return recentMessage;
      }));
      setIsLoading(false);
    };

    fetchRecentMessages();

    if (hubConnection) {
      hubConnection.on("ReceiveMessage", (user: string, content: string) => {
        const newMessage = { user, content };
        setRecentMessages(prev => [...prev, newMessage]);
      });

      return () => {
        hubConnection.off("ReceiveMessage");
      };
    }
  }, [hubConnection]);

  return (
    <Container component="main" maxWidth="lg">
      <CssBaseline />
      <LatestMessages messages={recentMessages} />
      <Grid paddingY={5}>
        <TextField
          autoComplete="given-name"
          name="message"
          required
          fullWidth
          id="message"
          label="New message"
          value={message}
          onChange={handleInputChange}
          autoFocus />
      </Grid>
      <Button disabled={isLoading} variant="contained" onClick={handleSendMessage}>
        Send Message
      </Button>
    </Container>
  )
}