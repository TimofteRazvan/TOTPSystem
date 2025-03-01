namespace TOTPSystem.Test.Mocks
{
    /// <summary>
    /// Class for the purpose of mocking the session to be used in unit testing the OTPController.
    /// </summary>
    public class MockSession : ISession
    {
        private Dictionary<string, byte[]> _sessionStorage = new Dictionary<string, byte[]>();
        public bool IsAvailable => true;
        public string Id => Guid.NewGuid().ToString();
        public IEnumerable<string> Keys => _sessionStorage.Keys;
        public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value);
        public void Set(string key, byte[] value) => _sessionStorage[key] = value;
        public void Remove(string key) => _sessionStorage.Remove(key);
        public void Clear() => _sessionStorage.Clear();

    }
}
