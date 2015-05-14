namespace Tweeter.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Tweet> tweets;
        private ICollection<Tweet> favoritedTweets;
        private ICollection<Tweet> reTweetedTweets; 
        private ICollection<Replay> replays;
        private ICollection<Report> reports;
        private ICollection<User> followers;
        private ICollection<User> following; 

        public User()
        {
            this.tweets = new HashSet<Tweet>();
            this.favoritedTweets = new HashSet<Tweet>();
            this.reTweetedTweets = new HashSet<Tweet>();
            this.reports = new HashSet<Report>();
            this.replays = new HashSet<Replay>();
            this.followers = new HashSet<User>();
            this.following = new HashSet<User>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FullName { get; set; }

        public virtual ICollection<Tweet> Tweets
        {
            get { return this.tweets; }
            set { this.tweets = value; }
        }

        public virtual ICollection<Tweet> FavoritedTweets
        {
            get { return this.favoritedTweets; }
            set { this.favoritedTweets = value; }
        }

        public virtual ICollection<Tweet> ReTweetedTweets
        {
            get { return this.reTweetedTweets; }
            set { this.reTweetedTweets = value; }
        }

        public virtual ICollection<Replay> Replays
        {
            get { return this.replays; }
            set { this.replays = value; }
        }

        public virtual ICollection<Report> Reports
        {
            get { return this.reports; }
            set { this.reports = value; }
        }

        public virtual ICollection<User> Followers
        {
            get { return this.followers; }
            set { this.followers = value; }
        }

        public virtual ICollection<User> Following
        {
            get { return this.following; }
            set { this.following = value; }
        }
    }
}
