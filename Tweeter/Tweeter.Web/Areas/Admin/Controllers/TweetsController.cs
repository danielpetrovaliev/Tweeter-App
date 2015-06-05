namespace Tweeter.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data.UnitOfWork;
    using InputModels;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models;
    using ViewModels.User;

    [Authorize(Roles = "Administrator")]
    public class TweetsController : BaseAdminController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GetAuthors()
        {
            var authors = this.Data
                .Users
                .All()
                .ToList()
                .Select(u => new SimpleUserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                });

            return this.Json(authors, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            var tweets = this.Data
                .Tweets
                .All()
                .Project()
                .To<TweetInputModel>();

            return Json(tweets.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, 
            TweetInputModel tweetModel)
        {
            if (tweetModel != null && ModelState.IsValid)
            {
                var tweet = Mapper.Map<Tweet>(tweetModel);

                this.Data.Tweets.Add(tweet);
                this.Data.SaveChanges();
            }

            return Json(new[] { tweetModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request,
            TweetInputModel tweetModel)
        {
            if (tweetModel != null && ModelState.IsValid)
            {
                var tweet = this.Data
                    .Tweets
                    .All()
                    .FirstOrDefault(t => t.Id == tweetModel.Id);

                tweet.Text = tweetModel.Text;
                tweet.AuthorId = tweetModel.AuthorId;
                tweet.CreatedOn = tweetModel.CreatedOn;

                this.Data.SaveChanges();
            }

            return Json(new[] { tweetModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, 
            TweetInputModel tweetModel)
        {
            if (tweetModel != null && ModelState.IsValid)
            {
                var tweet = this.Data
                    .Tweets
                    .All()
                    .FirstOrDefault(t => t.Id == tweetModel.Id);

                this.Data.Tweets.Remove(tweet);
                this.Data.SaveChanges();
            }

            return Json(new[] { tweetModel }.ToDataSourceResult(request, ModelState));
        }

        public TweetsController(ITweeterData data)
            : base(data)
        {
        }
    }
}