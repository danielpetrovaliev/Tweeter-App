namespace Tweeter.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Mapping;
    using Models;

    public class TweetViewModel : IMapFrom<Tweet>
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public string AuthorId { get; set; }

        public virtual UserViewModel Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<ReplayViewModel> Replays { get; set; }
         
        public virtual ICollection<UserViewModel> UsersFavorites { get; set; }

        public virtual ICollection<UserViewModel> UsersReTweets { get; set; }

        public virtual ICollection<ReportViewModel> Reports { get; set; }
    }
}