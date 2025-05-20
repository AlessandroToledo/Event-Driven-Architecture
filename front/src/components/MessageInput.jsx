function MessageInput({ currentMessage, setCurrentMessage, handleSend }) {
  return (
    <div>
      <input
        type="text"
        placeholder="Digite sua mensagem"
        value={currentMessage}
        onChange={(e) => setCurrentMessage(e.target.value)}
        onKeyDown={(e) => {
          if (e.key === 'Enter') handleSend();
        }}
        style={{ width: '70%', marginRight: '10px' }}
      />
      <button onClick={handleSend}>Enviar</button>
    </div>
  );
}

export default MessageInput;