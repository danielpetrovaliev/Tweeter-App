namespace Tweeter.Web.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Mapping;
    using Models;

    public class NotificationInputModel : IMapTo<Notification>, IMapFrom<Notification>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsChecked { get; set; }

        [UIHint("UserEditor")]
        public string UserId { get; set; }
    }
}