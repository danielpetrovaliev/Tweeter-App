namespace Tweeter.Data.UnitOfWork
{
    using Models;
    using Repositories;

    public interface ITweeterData
    {
        IRepository<User> Users { get; }

        IRepository<Notification> Notifications { get; }

        IRepository<Replay> Replays { get; }

        IRepository<Report> Reports { get; }

        IRepository<Tweet> Tweets { get; }
 
        int SaveChanges();
    }
}