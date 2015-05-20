namespace Tweeter.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.UnitOfWork;
    using ViewModels.Tweet;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var tweetsOfFlowers = this.UserProfile
                    .Followings
                    .SelectMany(u => u.Tweets)
                    .AsQueryable().Project().To<TweetViewModel>();

                return View(tweetsOfFlowers);
            }

            var tweets = this.Data.Tweets.All()
                    .Include(t => t.Author).Project().To<TweetViewModel>();

            return View(tweets);
        }

        public HomeController(ITweeterData data)
            : base(data)
        {
        }
    }
}