namespace Tweeter.Web.ViewModels.Tweet
{
    using System.Collections.Generic;
    using Replay;
    using Report;
    using User;

    public class TweetViewModel : SimpleTweetViewModel
    {
        public virtual ICollection<ReplayViewModel> Replays { get; set; }
         
        public virtual ICollection<SimpleUserViewModel> UsersFavorites { get; set; }

        public virtual ICollection<SimpleUserViewModel> UsersReTweets { get; set; }

        public virtual ICollection<ReportViewModel> Reports { get; set; }

        public SimpleTweetViewModel CreateModel { get; set; }
    }
}