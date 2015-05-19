namespace Tweeter.Web.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;

    [Authorize]
    public class UsersController : BaseController
    {
        public ActionResult ShowProfile()
        {
            return View(this.UserProfile);
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