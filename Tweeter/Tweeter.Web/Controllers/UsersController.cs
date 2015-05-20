namespace Tweeter.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Data.UnitOfWork;
    using Models;
    using ViewModels.User;

    [Authorize]
    public class UsersController : BaseController
    {
        public ActionResult ShowProfile(string id)
        {
            if (id != null)
            {
                var user = this.Data.Users.Find(id);

                if (user == null)
                {
                    return this.HttpNotFound();
                }

                return View(Mapper.Map<User, UserViewModel>(user));
            }

            return View(Mapper.Map<User, UserViewModel>(this.UserProfile));
        }

        public ActionResult Notifications()
        {
            var notifications = this.UserProfile.Notifications;
            return View(notifications);
        }

        public UsersController(ITweeterData data) 
            : base(data)
        {
        }
    }
}