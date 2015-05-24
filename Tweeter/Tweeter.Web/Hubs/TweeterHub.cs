namespace Tweeter.Web.Hubs
{
    using System.Data.Entity;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.UnitOfWork;
    using Microsoft.AspNet.SignalR;
    using ViewModels.Tweet;

    public class TweeterHub : Hub
    {
        public static ITweeterData Data
        {
            get { return new TweeterData(new TweeterDbContext()); }
        }

        public void JoinRoom(string room)
        {
            Groups.Add(Context.ConnectionId, room);
            Clients.Caller.joinRoom(room);
        }

        public void SendMessageToRoom(string message, string[] rooms)
        {
            var msg = string.Format("{0}: {1}", Context.ConnectionId, message);

            for (int i = 0; i < rooms.Length; i++)
            {
                Clients.Group(rooms[i]).addMessage(msg);
            }
        }
    }
}