namespace Tweeter.Web.ViewModels.Replay
{
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Mapping;
    using Models;
    using Tweet;
    using User;

    public class ReplayViewModel : IMapFrom<Replay>
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public string AuthorId { get; set; }

        public virtual SimpleUserViewModel Author { get; set; }

        public int TweetId { get; set; }

        public virtual SimpleTweetViewModel Tweet { get; set; }
    }
}
