function UsernameInput({ username, setUsername }) {
  return (
    <div style={{ marginBottom: '10px' }}>
      <label>
        Seu nome:
        <input
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          style={{ marginLeft: '10px', width: '70%' }}
        />
      </label>
    </div>
  );
}

export default UsernameInput;