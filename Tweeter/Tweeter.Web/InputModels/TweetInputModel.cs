namespace Tweeter.Web.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Mapping;
    using Models;

    public class TweetInputModel : IMapTo<Tweet>, IMapFrom<Tweet>
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [UIHint("AuthorEditor")]
        public string AuthorId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } 
    }
}