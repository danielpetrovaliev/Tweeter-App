namespace Tweeter.Web.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data.UnitOfWork;
    using Models;
    using ViewModels;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //if (this.User.Identity.IsAuthenticated)
            //{
            //    var tweetsOfFlowers = this.UserProfile
            //        .Following.
            //        .Select(u => u.Tweets)
            //        .Cast<Tweet>()
            //        .ToList();

            //    return View(tweetsOfFlowers);
            //}
            
            var tweets = this.Data.Tweets.All()
                    .Include(t => t.Author).ToList();

            var viewTweets = Mapper.Map<List<Tweet>, List<TweetViewModel>>(tweets);

            return View(viewTweets);
        }

        public HomeController(ITweeterData data)
            : base(data)
        {
        }
    }
}