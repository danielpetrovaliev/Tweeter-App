namespace Tweeter.Web.Hubs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Data.UnitOfWork;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Models;

    [Authorize]
    [HubName("tweeterHub")]
    public class TweeterHub : Hub
    {
        private Dictionary<string, string> connectedUsers = new Dictionary<string, string>();

        public static ITweeterData Data
        {
            get { return new TweeterData(new TweeterDbContext()); }
        }

        /*public override Task OnConnected()
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            connectedUsers.Add(userName, connectionId);

            return base.OnConnected();
        }*/

    }
}