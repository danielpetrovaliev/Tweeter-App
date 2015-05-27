namespace Tweeter.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.UnitOfWork;
    using ViewModels.Notification;
    using ViewModels.User;
    using WebGrease.Css.Extensions;

    [Authorize]
    public class UsersController : BaseController
    {
        public ActionResult ShowProfile(string id)
        {
            var user = this.Data
                .Users
                .All()
                .Project()
                .To<UserViewModel>()
                .FirstOrDefault(u => u.Id == this.UserProfile.Id);

            if (id != null)
            {
                user = this.Data
                    .Users
                    .All()
                    .Project()
                    .To<UserViewModel>()
                    .FirstOrDefault(u => u.Id == this.UserProfile.Id);

                if (user == null)
                {
                    return this.HttpNotFound();
                }

                return View(user);
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult GetNotificationsCount()
        {
            var notificationsCount = this.UserProfile.Notifications.Count(n => n.IsChecked == false).ToString();
            return this.Content(notificationsCount);
        }

        public ActionResult Notifications()
        {
            var notifications = this.Data
                .Notifications
                .All()
                .Include(n => n.User)
                .Where(n => n.UserId == this.UserProfile.Id)
                .OrderBy(n => n.IsChecked)
                .ThenByDescending(n => n.Date)
                .Project()
                .To<NotificationViewModel>().ToList();

            // Change status of notifications in database after we got it
            this.Data
                .Notifications
                .All()
                .Where(n => n.UserId == this.UserProfile.Id)
                .ForEach(n => n.IsChecked = true);
            this.Data.SaveChanges();

            return View(notifications);
        }

        public UsersController(ITweeterData data)
            : base(data)
        {
        }
    }
}