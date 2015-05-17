namespace Tweeter.Web.ViewModels
{
    using System;
    using Infrastructure.Mapping;
    using Models;

    public class ReportViewModel : IMapFrom<Report>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int TweetId { get; set; }

        public DateTime Created { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
