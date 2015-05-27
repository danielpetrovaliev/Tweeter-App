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
                tweets = this.Data
                    .Tweets
                    .All()
                    .Where(t => t.AuthorId == this.UserProfile.Id)
                    .Include(t => t.UsersFavorites)
                    .Include(t => t.Author)
                    .Include(t => t.Replays)
                    .Include(t => t.Reports)
                    .Include(t => t.UsersReTweets)
                    .OrderByDescending(t => t.CreatedOn)
                    .Project()
                    .To<TweetViewModel>();
            }
            else
            {
                tweets = this.Data
                .Tweets
                .All()
                .Include(t => t.UsersFavorites)
                .Include(t => t.Author)
                .Include(t => t.Replays)
                .Include(t => t.Reports)
                .Include(t => t.UsersReTweets)
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