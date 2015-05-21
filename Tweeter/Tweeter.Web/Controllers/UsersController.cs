namespace Tweeter.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.UnitOfWork;
    using ViewModels.Notification;
    using ViewModels.User;

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

        public ActionResult Notifications()
        {
            var notifications = this.Data
                .Notifications
                .All()
                .Where(n => n.UserId == this.UserProfile.Id)
                .OrderBy(n => n.IsChecked)
                .ThenByDescending(n => n.Date)
                .Project()
                .To<NotificationViewModel>();

            return View(notifications);
        }

        public UsersController(ITweeterData data)
            : base(data)
        {
        }
    }
}