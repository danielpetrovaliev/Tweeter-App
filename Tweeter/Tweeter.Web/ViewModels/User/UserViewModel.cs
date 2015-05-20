namespace Tweeter.Web.ViewModels.User
{
    using System.Collections.Generic;
    using Notification;
    using Replay;
    using Report;
    using Tweet;

    public class UserViewModel : SimpleUserViewModel
    {
        public virtual ICollection<SimpleTweetViewModel> Tweets { get; set; }

        public virtual ICollection<SimpleTweetViewModel> FavoritedTweets { get; set; }

        public virtual ICollection<SimpleTweetViewModel> ReTweetedTweets { get; set; }

        public virtual ICollection<ReplayViewModel> Replays { get; set; }

        public virtual ICollection<ReportViewModel> Reports { get; set; }

        public virtual ICollection<SimpleUserViewModel> Followers { get; set; }

        public virtual ICollection<SimpleUserViewModel> Followings { get; set; }

        public virtual ICollection<NotificationViewModel> Notifications { get; set; }
    }
}