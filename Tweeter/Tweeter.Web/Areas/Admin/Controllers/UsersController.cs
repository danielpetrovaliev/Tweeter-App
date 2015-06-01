namespace Tweeter.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using Infragistics.Web.Mvc;
    using Web.Controllers;

    public class UsersController : BaseController
    {
        public ActionResult Index()
        {
            var users = this.Data.Users.All();

            var grid = new GridModel{DataSource = users};

            return this.View(grid);
        }

        public UsersController(ITweeterData data)
            : base(data)
        {
        }
    }
}