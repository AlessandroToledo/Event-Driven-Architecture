import React, { createContext, useContext, useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';

const ConnectionContext = createContext(null);

export const ConnectionProvider = ({ children }) => {
  const [connection, setConnection] = useState(null);

  useEffect(() => {
    const apiUrl = import.meta.env.VITE_API_URL;
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${apiUrl}/chatHub`, {withCredentials: true})
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);

    newConnection.start()
      .then(() => console.log('SignalR conectado'))
      .catch(e => console.error('Erro SignalR:', e));

    return () => {
      newConnection.stop();
    };
  }, []);

  return (
    <ConnectionContext.Provider value={connection}>
      {children}
    </ConnectionContext.Provider>
  );
};

export const useConnection = () => useContext(ConnectionContext);