namespace Tweeter.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using Infragistics.Web.Mvc;
    using ViewModels.User;
    using Web.Controllers;

    public class UsersController : BaseController
    {
        public ActionResult Index()
        {
            var users = this.Data.Users.All().Select(u => new UserViewModel
            {
                UserName = u.UserName,
                Id = u.Id,
                Email = u.Email
            }).ToList().AsQueryable();

            var grid = new GridModel { DataSource = users, AutoGenerateColumns = true};

            return this.View(grid);
        }

        public UsersController(ITweeterData data)
            : base(data)
        {
        }
    }
}