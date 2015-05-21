namespace Tweeter.Web.ViewModels.Notification
{
    using System;
    using Infrastructure.Mapping;
    using Models;
    using User;

    public class NotificationViewModel : IMapFrom<Notification>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsChecked { get; set; }

        public string UserId { get; set; }

        public virtual SimpleUserViewModel User { get; set; }
    }
}
