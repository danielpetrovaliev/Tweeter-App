namespace Tweeter.Web.ViewModels.User
{
    using Infrastructure.Mapping;
    using Models;

    public class SimpleUserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; } 
    }
}