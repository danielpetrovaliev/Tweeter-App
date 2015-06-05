namespace Tweeter.Web.ViewModels.Tweet
{
    using System.Collections.Generic;
    using Replay;
    using Report;
    using User;

    public class TweetViewModel : SimpleTweetViewModel
    {
        public ICollection<ReplayViewModel> Replays { get; set; }
         
        public ICollection<SimpleUserViewModel> UsersFavorites { get; set; }

        public ICollection<SimpleUserViewModel> UsersReTweets { get; set; }

        public ICollection<ReportViewModel> Reports { get; set; }
    }
}