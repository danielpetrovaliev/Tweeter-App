namespace Tweeter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tweet
    {
        private ICollection<User> usersFavorites;
        private ICollection<User> usersReTweets;

        public Tweet()
        {
            this.usersFavorites = new HashSet<User>();
            this.usersReTweets = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<Replay> Replays { get; set; }

        public virtual ICollection<User> UsersFavorites
        {
            get { return this.usersFavorites; }
            set { this.usersFavorites = value; }
        }

        public virtual ICollection<User> UsersReTweets
        {
            get { return this.usersReTweets; }
            set { this.usersReTweets = value; }
        }
    }
}