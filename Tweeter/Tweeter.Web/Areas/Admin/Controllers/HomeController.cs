namespace Tweeter.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;

    [Authorize(Roles = "Administrator")]
    public class HomeController : BaseAdminController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public HomeController(ITweeterData data) 
            : base(data)
        {
        }
    }
}