// SignalRContext.tsx
import React, { createContext, useState, useEffect } from 'react';
import * as signalR from '@microsoft/signalr';
import { Constants } from '../utils/constants';

export interface SignalRContextType {
  connection: signalR.HubConnection | null;
  isConnected: boolean;
}


type SignalRProviderProps = {
  children?: React.ReactNode
};

const SignalRContext = createContext<SignalRContextType | undefined>(undefined);

const SignalRProvider: React.FC<SignalRProviderProps> = ({ children }) => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [isConnected, setIsConnected] = useState(false);

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${Constants.BASE_URL}chathub`)
      .build();

    newConnection
      .start()
      .then(() => {
        setIsConnected(true);
      })
      .catch((error) => {
        console.error('SignalR connection failed:', error);
      });

    setConnection(newConnection);

    return () => {
      if (connection) {
        connection.stop();
      }
    };
  }, []);

  const contextValue: SignalRContextType = { connection, isConnected };

  return (
    <SignalRContext.Provider value={contextValue}>
      {children}
    </SignalRContext.Provider>
  );
}

export { SignalRContext, SignalRProvider };
