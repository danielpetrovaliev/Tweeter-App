namespace Tweeter.Web.ViewModels
{
    using System.Collections.Generic;
    using Infrastructure.Mapping;
    using Models;

    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public virtual ICollection<TweetViewModel> Tweets { get; set; }

        public virtual ICollection<TweetViewModel> FavoritedTweets { get; set; }

        public virtual ICollection<TweetViewModel> ReTweetedTweets { get; set; }

        public virtual ICollection<ReplayViewModel> Replays { get; set; }

        public virtual ICollection<ReportViewModel> Reports { get; set; }

        public virtual ICollection<UserViewModel> Followers { get; set; }

        public virtual ICollection<UserViewModel> Following { get; set; }

        public virtual ICollection<NotificationViewModel> Notifications { get; set; }
    }
}