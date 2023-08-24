import React from 'react';
import { Grid, List, ListItem, ListItemText } from '@mui/material';

export type Message = {
  user: string;
  content: string;
};

export type LatestMessagesProps = {
  messages: Message[];
};

const LatestMessages: React.FC<LatestMessagesProps> = ({ messages }) => {
  const latestMessages = messages.slice(-50);

  return (
    <Grid sx={{
      width: '100%',
      minHeight: '80vh',
      overflowY: 'auto'
    }}>
      <List>
        {latestMessages.map((message, index) => (
          <ListItem key={index}>
            <ListItemText
              primary={message.user}
              secondary={message.content}
            />
          </ListItem>
        ))}
      </List>
    </Grid>
  );
}

export default LatestMessages;
