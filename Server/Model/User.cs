namespace Server.Model
{
    internal class User
    {
        public uint Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public User(uint id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }
    }
}
