namespace Tweeter.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;

    public class TweeterDbContext : IdentityDbContext<User>, ITweeterDbContext
    {
        public TweeterDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TweeterDbContext, Configuration>());
        }

        public static TweeterDbContext Create()
        {
            return new TweeterDbContext();
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tweets)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("UserId");
                    x.MapRightKey("TweetId");
                    x.ToTable("UsersToTweets");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReTweetedTweets)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("UserId");
                    x.MapRightKey("ReTweetedTweetId");
                    x.ToTable("UsersToReTweetedTweet");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.FavoritedTweets)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("UserId");
                    x.MapRightKey("TweetId");
                    x.ToTable("UsersToFavoritedTweets");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Followers)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("UserId");
                    x.MapRightKey("FollowerId");
                    x.ToTable("UsersToFollowers");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Following)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("UserId");
                    x.MapRightKey("FollowingId");
                    x.ToTable("UsersToFollowings");
                });
        }

        public virtual IDbSet<Tweet> Tweets { get; set; }

        public virtual IDbSet<Notification> Notifications { get; set; }

        public virtual IDbSet<Replay> Replays { get; set; }

        public virtual IDbSet<Report> Reports { get; set; }
    }
}
