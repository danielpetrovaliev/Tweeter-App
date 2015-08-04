namespace Tweeter.Web.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data.UnitOfWork;
    using InputModels;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models;

    [Authorize(Roles = "Administrator")]
    public class UsersController : BaseAdminController
    {
        public UsersController(ITweeterData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            var users = this.Data
                .Users
                .All()
                .Project()
                .To<UserInputModel>();

            return this.Json(users.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, UserInputModel userModel)
        {
            if (userModel != null && this.ModelState.IsValid)
            {
                // I create instace of new user because with automapper EF throws exception for validation errors   
                var user = new User
                {
                    UserName = userModel.UserName,
                    Email = userModel.Email,
                    FullName = userModel.FullName,
                    PasswordHash = userModel.PasswordHash
                };

                var userManager = this.HttpContext.GetOwinContext().
                    GetUserManager<ApplicationUserManager>();

                userManager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 2,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                };

                var userCreateResult = userManager.Create(user, user.PasswordHash);
                if (!userCreateResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", userCreateResult.Errors));
                }

                // Set role
                this.AddOrRemoveAdministratorRoleForUser(user.Id, userModel.IsAdmin);

                this.Data.SaveChanges();

                // Sets return model data
                userModel.PasswordHash = user.PasswordHash;
                userModel.Id = user.Id;
            }

            return this.Json(new[] { userModel }.ToDataSourceResult(request, this.ModelState));
        }

        private void AddOrRemoveAdministratorRoleForUser(string id, bool isAdmin)
        {
            var userManager = this.HttpContext.GetOwinContext().
                GetUserManager<ApplicationUserManager>();
            if (isAdmin)
            {
                var addAdminRoleResult = userManager.AddToRole(id, "Administrator");
                if (!addAdminRoleResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
                }
            }
            else
            {
                var removeAdminRoleResult = userManager.RemoveFromRole(id, "Administrator");
                if (!removeAdminRoleResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", removeAdminRoleResult.Errors));
                }
            }
            
        }

        [HttpPost]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, UserInputModel userModel)
        {
            if (userModel != null && this.ModelState.IsValid)
            {
                var userManager = this.HttpContext.GetOwinContext().
                    GetUserManager<ApplicationUserManager>();

                var userDb = this.Data
                            .Users
                            .All()
                            .FirstOrDefault(u => u.Id == userModel.Id);

                if (userDb != null)
                {
                    // Hash password with default user manager password hasher
                    userDb.PasswordHash = userManager.PasswordHasher.HashPassword(userModel.PasswordHash);
                    userDb.UserName = userModel.UserName;
                    userDb.Email = userModel.Email;
                    userDb.FullName = userModel.FullName;

                    this.Data.SaveChanges();

                    // Set role
                    this.AddOrRemoveAdministratorRoleForUser(userModel.Id, userModel.IsAdmin);

                    // Set new password to input model
                    userModel.PasswordHash = userDb.PasswordHash;
                }
                else
                {
                    return this.HttpNotFound();
                }
            }

            return this.Json(new[] { userModel }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, UserInputModel userModel)
        {
            if (userModel != null)
            {
                var user = Mapper.Map<User>(userModel);
                this.Data.Users.Remove(user);
                this.Data.SaveChanges();
            }

            return this.Json(new[] { userModel }.ToDataSourceResult(request, this.ModelState));
        }
    }
}