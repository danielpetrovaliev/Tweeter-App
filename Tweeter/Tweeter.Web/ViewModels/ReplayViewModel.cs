namespace Tweeter.Web.ViewModels
{
    using Infrastructure.Mapping;
    using Models;

    public class ReplayViewModel : IMapFrom<Replay>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string AuthorId { get; set; }

        public virtual UserViewModel Author { get; set; }
    }
}
