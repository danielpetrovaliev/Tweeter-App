namespace Tweeter.Web.ViewModels
{
    using Infrastructure.Mapping;
    using Models;

    public class NotificationViewModel : IMapFrom<Notification>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsChecked { get; set; }

        public string UserId { get; set; }
    }
}
