namespace Core.DTO.Jwt
{
    public sealed class Token
    {
        public int Id { get; }
        public string AuthToken { get; }
        public long ExpiresIn { get; }

        public Token(int id, string authToken, long expiresIn)
        {
            Id = id;
            AuthToken = authToken;
            ExpiresIn = expiresIn;
        }
    }
}
