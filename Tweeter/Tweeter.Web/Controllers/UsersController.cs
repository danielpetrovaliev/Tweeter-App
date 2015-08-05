namespace Tweeter.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.UnitOfWork;
    using PagedList;
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
                .Include(u => u.Followings)
                .Include(u => u.Followers)
                .Include(u => u.Tweets)
                .Project()
                .To<UserViewModel>()
                .FirstOrDefault(u => u.Id == this.UserProfile.Id);

            this.ViewBag.isIFollow = true;

            if (id != null)
            {
                user = this.Data
                    .Users
                    .All()
                    .Include(u => u.Followings)
                    .Include(u => u.Followers)
                    .Include(u => u.Tweets)
                    .Project()
                    .To<UserViewModel>()
                    .FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return this.HttpNotFound();
                }

                this.ViewBag.isIFollow = user.Followers.Any(u => u.Id == this.UserProfile.Id);
                if (this.UserProfile.Id == id)
                {
                    this.ViewBag.isIFollow = true;
                }

                return this.View(user);
            }

            return this.View(user);
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

            return this.View(notifications);
        }

        public UsersController(ITweeterData data)
            : base(data)
        {
        }

        public ActionResult Update(string ig_transactions)
        {

            throw new HttpException();
            return null;
        }

        [HttpGet]
        public ActionResult Search(string query, int? page)
        {
            var users = this.Data
                .Users
                .All()
                .Where(u => u.UserName.Contains(query) || u.Email.Contains(query))
                .OrderBy(u => u.UserName)
                .Project()
                .To<SimpleUserViewModel>();

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return this.View(users.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Follow(string userId)
        {
            var user = this.Data
                .Users
                .All()
                .FirstOrDefault(u => u.Id == userId);

            var currUser = this.Data
                .Users
                .All()
                .FirstOrDefault(u => u.Id == this.UserProfile.Id);

            user.Followers.Add(currUser);
            currUser.Followings.Add(user);
            this.Data.SaveChanges();

            return this.RedirectToAction("ShowProfile", new {id = userId});
        }
    }
}