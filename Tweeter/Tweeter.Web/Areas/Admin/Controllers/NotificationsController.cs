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
    using ViewModels.User;
    using Notification = Models.Notification;

    public class NotificationsController : BaseAdminController
    {
        public NotificationsController(ITweeterData data) 
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GetAuthors()
        {
            var notifications = this.Data
                .Users
                .All()
                .ToList()
                .Select(u => new SimpleUserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName
                });

            return this.Json(notifications, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            var tweets = this.Data
                .Notifications
                .All()
                .Project()
                .To<NotificationInputModel>();

            return this.Json(tweets.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request,
            NotificationInputModel notificationModel)
        {
            if (notificationModel != null && this.ModelState.IsValid)
            {
                var notification = Mapper.Map<Notification>(notificationModel);

                this.Data.Notifications.Add(notification);
                this.Data.SaveChanges();
            }

            return this.Json(new[] { notificationModel }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request,
            NotificationInputModel notificationModel)
        {
            if (notificationModel != null && this.ModelState.IsValid)
            {
                var notification = Mapper.Map<Notification>(notificationModel);
                this.Data.Notifications.Update(notification);
                this.Data.SaveChanges();
            }

            return this.Json(new[] { notificationModel }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request,
            TweetInputModel tweetModel)
        {
            if (tweetModel != null && this.ModelState.IsValid)
            {
                var tweet = this.Data
                    .Tweets
                    .All()
                    .FirstOrDefault(t => t.Id == tweetModel.Id);

                this.Data.Tweets.Remove(tweet);
                this.Data.SaveChanges();
            }

            return this.Json(new[] { tweetModel }.ToDataSourceResult(request, this.ModelState));
        }
    }
}