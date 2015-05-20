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
