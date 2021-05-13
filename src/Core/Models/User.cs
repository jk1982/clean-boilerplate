namespace Core.Models
{
    public class User
    {
        public User()
        {

        }

        public User(int id, string email, string passwordHash, bool blocked)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            Blocked = blocked;
        }

        public User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        public int Id { get; }
        public string Email { get; }
        public string PasswordHash { get; }
        public bool Blocked { get; }
    }
}
