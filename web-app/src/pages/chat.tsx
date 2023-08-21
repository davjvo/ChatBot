import Container from "@mui/material/Container";
import CssBaseline from "@mui/material/CssBaseline";
import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";
import React, { useContext, useState } from "react";
import Button from "@mui/material/Button";
import { useLocation } from "react-router-dom";
import { SignalRContext } from "../context/SignalRContext";

export const Chat: React.FC = () => {
  const [message, setMessage] = useState('');
  const { state } = useLocation();
  const { user } = state;
  const { connection } = useContext(SignalRContext)!;

  connection && connection.on("ReceiveMessage", (user: string, message: string) => {
    console.log(`${user}: ${message}`);
  });

  const handleSendMessage = (_: React.MouseEvent<HTMLElement>) => {
    connection && connection.invoke('SendMessage', user, message)
  }

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setMessage(event.target.value);
  }

  return (
    <Container component="main" maxWidth="lg">
      <CssBaseline />
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
      <Button variant="contained" onClick={handleSendMessage}>
        Send Message
      </Button>
    </Container>
  )
}