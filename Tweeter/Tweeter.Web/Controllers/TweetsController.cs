namespace Tweeter.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.UnitOfWork;
    using Hubs;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using Models;
    using ViewModels.Tweet;

    [System.Web.Mvc.Authorize]
    public class TweetsController : BaseController
    {
        private TweeterDbContext db = new TweeterDbContext();

        // GET: Tweets
        public ActionResult Index()
        {
            var tweets = db.Tweets.Include(t => t.Author);
            return View(tweets.ToList());
        }

        // GET: Tweets/Details/5
        public ActionResult GetPartialTweet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tweet = this.Data
                .Tweets
                .All()
                .Project()
                .To<TweetViewModel>()
                .FirstOrDefault(t => t.Id == id);

            if (tweet == null)
            {
                return HttpNotFound();
            }

            return View(tweet);
        }

        [HttpPost]
        public ActionResult FavoriteTweet(int id)
        {
            var tweet = this.Data
                .Tweets
                .All()
                .Include(t => t.Author)
                .Include(t => t.UsersFavorites)
                .Include(t => t.Author.Notifications)
                .FirstOrDefault(t => t.Id == id);

            tweet.UsersFavorites.Add(this.UserProfile);
            this.Data.SaveChanges();

            tweet.Author.Notifications.Add(new Notification()
            {
                Text = this.UserProfile.UserName + " likes you tweet - " + tweet.Id
            });
            this.Data.SaveChanges();

            IncreaseNotifications(tweet.Author);

            return this.Content(tweet.UsersFavorites.Count + "");
        }

        [HttpPost]
        public ActionResult ReportTweet(int id)
        {
            var tweet = this.Data
                .Tweets
                .All()
                .Include(t => t.Author)
                .Include(t => t.Author.Notifications)
                .FirstOrDefault(t => t.Id == id);
            
            var report = new Report()
            {
                Text = "New Report - TODO: add form for Report !",
                TweetId = id,
                UserId = this.UserProfile.Id
            };
            tweet.Reports.Add(report);
            this.Data.SaveChanges();

            tweet.Author.Notifications.Add(new Notification()
            {
                Text = this.UserProfile.UserName + " added report with text - " + report.Text
            });
            this.Data.SaveChanges();

            IncreaseNotifications(tweet.Author);

            return this.Content(tweet.Reports.Count + "");
        }

        [NonAction]
        private void IncreaseNotifications(User author)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<TweeterHub>();
            var notificationsCount = author.Notifications.Count(n => n.IsChecked == false);
            context.Clients.User(author.UserName).increaseNotifications(notificationsCount);
        }

        // GET: Tweets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tweet = this.Data
                .Tweets
                .All()
                .Project()
                .To<TweetViewModel>()
                .FirstOrDefault(t => t.Id == id);

            if (tweet == null)
            {
                return HttpNotFound();
            }

            return View(tweet);
        }

        // GET: Tweets/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: Tweets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TweetViewModel tweet)
        {
            if (this.ModelState.IsValid)
            {
                tweet.AuthorId = this.User.Identity.GetUserId();

                var newTweet = new Tweet() {AuthorId = tweet.AuthorId, Text = tweet.Text};
                db.Tweets.Add(newTweet);
                db.SaveChanges();
                
                // Show Tweet to all followers
                var context = GlobalHost.ConnectionManager.GetHubContext<TweeterHub>();
                var usernames = this.UserProfile.Followers.Select(f => f.UserName).ToList();
                context.Clients.Users(usernames).showTweet(newTweet.Id);


                this.TempData["message"] = "Tweet added successfully.";
                this.TempData["isMessageSuccess"] = true;

                return RedirectToAction("Index", "Home");
            }

            this.TempData["message"] = "There are problem with tweet adding.";
            this.TempData["isMessageSuccess"] = false;

            this.ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName", tweet.AuthorId);
            return View("Tweet/_CreateTweetPartial", tweet);
        }

        // GET: Tweets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tweet tweet = db.Tweets.Find(id);
            if (tweet == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName", tweet.AuthorId);
            return View(tweet);
        }

        // POST: Tweets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AuthorId,Text,CreatedOn")] Tweet tweet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tweet).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName", tweet.AuthorId);
            return View(tweet);
        }

        // GET: Tweets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tweet tweet = db.Tweets.Find(id);
            if (tweet == null)
            {
                return HttpNotFound();
            }
            return View(tweet);
        }

        // POST: Tweets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tweet tweet = db.Tweets.Find(id);
            db.Tweets.Remove(tweet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public TweetsController(ITweeterData data)
            : base(data)
        {
        }
    }
}
