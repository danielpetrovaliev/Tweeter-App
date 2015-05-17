namespace Tweeter.Web.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
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
            var tweets = this.Data.Tweets.All().Include(t => t.Author).Project().To<TweetViewModel>();
            Mapper.CreateMap<Tweet, TweetViewModel>();
            var mappedTweets = Mapper.Map<IEnumerable<TweetViewModel>>(tweets);


            return View(tweets);
        }

        public HomeController(ITweeterData data) 
            : base(data)
        {
        }
    }
}