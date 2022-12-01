namespace UI.Aws.ViewModels
{
    public class UserSignupViewModel
    {
        public string Name { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }

    }
    public class UserSearchViewModel
    {
        public UserSearchViewModel(int id, string name, string user, string pass)
        {
            Id = id;
            Name = name;
            User = user;
            Pass = pass;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }

    }
}
