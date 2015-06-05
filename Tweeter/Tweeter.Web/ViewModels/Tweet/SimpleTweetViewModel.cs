namespace Tweeter.Web.ViewModels.Tweet
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Mapping;
    using Models;
    using User;

    public class SimpleTweetViewModel : IMapFrom<Tweet>
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public string AuthorId { get; set; }

        public SimpleUserViewModel Author { get; set; }

        public DateTime CreatedOn { get; set; } 
    }
}