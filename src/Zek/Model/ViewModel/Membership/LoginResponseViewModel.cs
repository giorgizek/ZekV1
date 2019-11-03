namespace Zek.Model.ViewModel.Membership
{
    public class LoginResponseViewModel<TKey>
    {
        public TKey Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string[] Roles { get; set; }
    }

    public class LoginResponseViewModel : LoginResponseViewModel<int>
    {
    }
}
