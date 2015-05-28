namespace Tweeter.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.UnitOfWork;
    using PagedList;
    using ViewModels.Tweet;

    public class HomeController : BaseController
    {
        public ActionResult Index(int? page)
        {
            IQueryable<TweetViewModel> tweets;

            if (this.User.Identity.IsAuthenticated)
            {
                var currUser = this.Data
                    .Users
                    .All()
                    .Include(u => u.Followings)
                    .Include("Followings.Tweets")
                    .Include("Followings.Tweets.Replays")
                    .Include("Followings.Tweets.UsersFavorites")
                    .Include("Followings.Tweets.UsersReTweets")
                    .Include("Followings.Tweets.Reports")
                    .FirstOrDefault(u => u.Id == this.UserProfile.Id);

                tweets = currUser
                    .Followings
                    .SelectMany(f => f.Tweets)
                    .OrderByDescending(t => t.CreatedOn)
                    .AsQueryable()
                    .Project()
                    .To<TweetViewModel>();
            }
            else
            {
                tweets = this.Data
                .Tweets
                .All()
                .Include(t => t.Author)
                .OrderByDescending(t => t.CreatedOn)
                .Project()
                .To<TweetViewModel>();
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(tweets.ToPagedList(pageNumber, pageSize));
        }

        public HomeController(ITweeterData data)
            : base(data)
        {
        }
    }
}