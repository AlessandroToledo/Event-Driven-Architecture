import { useEffect, useRef } from 'react';

function MessageList({ messages }) {
  const messagesEndRef = useRef(null);

  useEffect(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
  }, [messages]);

  return (
    <div
      style={{
        border: '1px solid #ccc',
        padding: '10px',
        height: '200px',
        overflowY: 'auto',
        marginBottom: '10px',
      }}
    >
      {messages.length === 0 && <p>Nenhuma mensagem ainda.</p>}
      {messages.map((msg, index) => (
        <div key={index}>
          <strong>{msg.user}:</strong> {msg.text}
        </div>
      ))}
      <div ref={messagesEndRef} />
    </div>
  );
}

export default MessageList;