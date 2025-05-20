import { useState, useEffect } from 'react';
import { useConnection } from './Connection_Context';

import MessageList from './components/MessageList';
import UsernameInput from './components/UsernameInput';
import MessageInput from './components/MessageInput';
import swearwordFilter from "./utils/filter";

function App() {
  const [username, setUsername] = useState('');
  const [currentMessage, setCurrentMessage] = useState('');
  const [messages, setMessages] = useState([]);
  const connection = useConnection();


  useEffect(() => {
    if (!connection) return;

    connection.on('ReceiveMessage', (user, message) => {
      setMessages(prev => [...prev, { user, text: message }]);
    });

    return () => {
      connection.off('ReceiveMessage');
    };
  }, [connection]);

  const handleSend = async () => {
    if (!username || !currentMessage || !connection) return;

    var message = {
      User : username,
      Text : swearwordFilter.filter(currentMessage),
      Offensive: swearwordFilter.has(currentMessage)
    }

    try {
      await connection.invoke('SendMessage', message);
      setCurrentMessage('');
    } catch (err) {
      console.error('Erro ao enviar mensagem:', err);
    }
  };

  return (
    <div style={{ padding: '20px', fontFamily: 'Arial' }}>
      <h2>Mini Chat</h2>
        <UsernameInput username={username} setUsername={setUsername} />
        <MessageList messages={messages} />
        <MessageInput
          currentMessage={currentMessage}
          setCurrentMessage={setCurrentMessage}
          handleSend={handleSend}
        />
    </div>
  );
}

export default App;