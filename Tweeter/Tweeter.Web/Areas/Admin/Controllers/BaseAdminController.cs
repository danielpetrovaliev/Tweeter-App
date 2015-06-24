namespace Tweeter.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using Models;

    public class BaseAdminController : Controller
    {
        private ITweeterData data;
        private User userProfile;

        protected BaseAdminController(ITweeterData data)
        {
            this.Data = data;
        }

        protected BaseAdminController(ITweeterData data, User userProfile)
            : this(data)
        {
            this.UserProfile = userProfile;
        }

        public ITweeterData Data
        {
            get { return this.data; }
            private set { this.data = value; }
        }

        public User UserProfile
        {
            get { return this.userProfile; }
            private set { this.userProfile = value; }
        }
    }
}