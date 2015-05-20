namespace Tweeter.Web.ViewModels.Report
{
    using System;
    using Infrastructure.Mapping;
    using Models;
    using Tweet;
    using User;

    public class ReportViewModel : IMapFrom<Report>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int TweetId { get; set; }

        public SimpleTweetViewModel Tweet { get; set; }

        public DateTime Created { get; set; }

        public string UserId { get; set; }

        public virtual SimpleUserViewModel User { get; set; }
    }
}
