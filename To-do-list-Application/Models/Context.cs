namespace To_do_list_Application.Models
{
    public class Context
    {
        public class UserCredentials
        {
            public static bool KeepLoggedIn { get; internal set; }
            public required string Username { get; set; }
            public required string Password { get; set; }
           
        }

    }
}
