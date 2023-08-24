import React, { createContext, useState, useContext, useEffect } from "react";
import * as signalR from "@microsoft/signalr";
import { Constants } from "../utils/constants";

const SignalRContext = createContext<signalR.HubConnection | null>(null);

type SignalRProviderProps = {
  children: React.ReactNode;
};

export const SignalRProvider: React.FC<SignalRProviderProps> = ({ children }) => {
  const [hubConnection, setHubConnection] = useState<signalR.HubConnection | null>(null);

  useEffect(() => {
    const createHubConnection = async () => {
      const hubConnect = new signalR.HubConnectionBuilder()
        .withUrl(`${Constants.BASE_URL}chatHub`)
        .withAutomaticReconnect()
        .build();

      try {
        await hubConnect.start();
        console.log("Connection successful!");
        setHubConnection(hubConnect);
      } catch (err) {
        console.error("There was an error establishing the connection", err);
      }
    };

    createHubConnection();

    return () => {
      hubConnection?.stop();
    };
  }, []);

  return <SignalRContext.Provider value={hubConnection}>{children}</SignalRContext.Provider>;
};

export const useSignalR = () => {
  const context = useContext(SignalRContext);
  if (context === undefined) {
    throw new Error("useSignalR must be used within a SignalRProvider");
  }
  return context;
};
