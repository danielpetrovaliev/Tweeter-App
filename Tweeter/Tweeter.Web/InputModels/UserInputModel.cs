namespace Tweeter.Web.InputModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.ApplicationServices;
    using System.Web.Security;
    using AutoMapper;
    using Data;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class UserInputModel : IMapTo<User>, IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string FullName { get; set; }

        public bool IsAdmin { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            var context = new TweeterDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var administratorRoleId = roleManager
                .Roles
                .Where(r => r.Name == "Administrator")
                .Select(r => r.Id)
                .FirstOrDefault();

            configuration.CreateMap<User, UserInputModel>()
                .ForMember(m => m.IsAdmin, cfg => cfg.MapFrom(e => e.Roles.Any(r => r.RoleId == administratorRoleId)));
        }
    }
}