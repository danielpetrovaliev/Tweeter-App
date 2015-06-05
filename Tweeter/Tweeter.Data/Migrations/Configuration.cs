namespace Tweeter.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TweeterDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TweeterDbContext context)
        {
            if (!context.Users.Any())
            {
                AddUsers(context);
            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                AddAdministratorRoleWithUser(context);
            }
        }

        private void AddAdministratorRoleWithUser(TweeterDbContext context)
        {
            var admin = new User()
            {
                Email = "admin@admin.com",
                FullName = "Admin Adminov",
                UserName = "admin"
            };

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 2,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var password = admin.UserName;
            var userCreateResult = userManager.Create(admin, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
            context.SaveChanges();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole("Administrator"));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }

            // Add "admin" user to "Administrator" role
            var adminUser = context.Users.First(user => user.UserName == "admin");
            var addAdminRoleResult = userManager.AddToRole(adminUser.Id, "Administrator");
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }

        private static void AddUsers(TweeterDbContext context)
        {
            var gosho = new User()
            {
                Email = "gosho@gosho.com",
                FullName = "Gosho Goshev",
                UserName = "gosho"
            };

            var pesho = new User()
            {
                Email = "pesho@pesho.com",
                FullName = "Pesho Peshev",
                UserName = "pesho"
            };


            var users = new List<User>()
            {
                pesho,
                gosho
            };

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 2,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            foreach (var user in users)
            {
                var password = user.UserName;
                var userCreateResult = userManager.Create(user, password);
                if (!userCreateResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", userCreateResult.Errors));
                }
            }

            pesho.Followers.Add(gosho);
            gosho.Followings.Add(pesho);

            gosho.Followers.Add(pesho);
            pesho.Followings.Add(gosho);

            context.SaveChanges();
        }
    }
}
