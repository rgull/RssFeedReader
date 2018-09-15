using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNet.Identity;
using ReadRSSFeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ReadRSSFeed.Controllers
{
    [Authorize]
    public class FeedController : Controller
    {
        SubscriptionRepository _subscriptionRepo;
        PostRepository _postRepo;
        FeedViewModel model;
        public FeedController(SubscriptionRepository subscriptionRepo,
            PostRepository postRepo)
        {
            _subscriptionRepo = subscriptionRepo;
            _postRepo = postRepo;
            model = new FeedViewModel();
        }
        public ActionResult Index()
        {
            string currentUserId = HttpContext.User.Identity.GetUserId();
            var SubscriptionList = _subscriptionRepo.Get(x => x.CreatedBy == currentUserId);
            return View(SubscriptionList);
        }
        public ActionResult RSSFeeds(int id)
        {
            
            try
            {
                string currentUserId = HttpContext.User.Identity.GetUserId();
                var subscription = _subscriptionRepo.GetSingle(x => x.Id == id);
                model.SubscriptionId = subscription.Id;
                WebClient wclient = new WebClient();
                string RSSData = wclient.DownloadString(subscription.URL); 
                XDocument xml = XDocument.Parse(RSSData);
                var sub = (from x in xml.Descendants("channel")
                                          select new Subscription
                                          {
                                              LastPublishedDate = DateTime.Parse(((string)x.Element("lastBuildDate")))
                                          });
                var RSSFeedData = (from x in xml.Descendants("item")
                                   select new RSSFeed
                                   {
                                       Title = ((string)x.Element("title")),
                                       Link = ((string)x.Element("link")),
                                       Description = ((string)x.Element("description")),
                                       PubDate = ((string)x.Element("pubDate"))
                                   });
                model.RSSFeeds = RSSFeedData.ToList();
                if (sub.FirstOrDefault().LastPublishedDate != subscription.LastPublishedDate)
                {
                    subscription.LastPublishedDate = sub.FirstOrDefault().LastPublishedDate;
                    if (_subscriptionRepo.Update(subscription))
                    {
                        foreach (var item in model.RSSFeeds)
                        {
                            Post post = new Post();
                            post.Author = item.Author;
                            post.Body = item.Description;
                            post.SubscriptionId = id;
                            post.PostDate = DateTime.Parse(item.PubDate);
                            post.Link = item.Link;
                            post.Title = item.Title;
                            post.CreatedBy = currentUserId;
                            _postRepo.Insert(post);
                        }
                    }
                }
                return PartialView(model);
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
                model = new FeedViewModel();
               
                return PartialView(model);
            }
        }

        public ActionResult RSSFeedsRemove(int id)
        {
            try
            {
                string currentUserId = HttpContext.User.Identity.GetUserId();
                var delete = _subscriptionRepo.Delete(id);
                if (delete)
                {
                    TempData["Success"] = "Unsubscribed Succesfully!";
                }
                return PartialView("RSSFeeds",model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                model = new FeedViewModel();

                return PartialView("RSSFeeds",model);
            }
        }

        public ActionResult RSSFeedsAll()
        {
            try
            {
                string currentUserId = HttpContext.User.Identity.GetUserId();
                if (currentUserId != null) {
                    List<RSSFeed> feeds = new List<RSSFeed>();
                    var posts = _postRepo.Get(x => x.CreatedBy == currentUserId  );

                    foreach (var item in posts)
                    {
                        RSSFeed feed = new RSSFeed();
                        feed.Author = item.Author;
                        feed.Description = item.Body;
                        feed.PubDate = item.PostDate.ToString();
                        feed.Title = item.Title;
                        feed.Link = item.Link;
                        feed.PubDate = item.PostDate.ToString();
                        feeds.Add(feed);

                    }
                    model.RSSFeeds = feeds;
                }
                //model.SubscriptionId = id;
                return PartialView("RSSFeeds", model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                model = new FeedViewModel();
                return PartialView(model);
            }
        }

        public ActionResult RSSFeedsSearch(string filter)
        {
            try
            {
                string currentUserId = HttpContext.User.Identity.GetUserId();
                if (currentUserId != null)
                {
                    List<RSSFeed> feeds = new List<RSSFeed>();
                    var posts = _postRepo.Get(x => (x.Body.Contains(filter) || x.Title.Contains(filter) || x.Author.Contains(filter)) && x.CreatedBy == currentUserId);

                    foreach (var item in posts)
                    {
                        RSSFeed feed = new RSSFeed();
                        feed.Author = item.Author;
                        feed.Description = item.Body;
                        feed.PubDate = item.PostDate.ToString();
                        feed.Title = item.Title;
                        feed.Link = item.Link;
                        feed.PubDate = item.PostDate.ToString();
                        feeds.Add(feed);

                    }
                    model.RSSFeeds = feeds;
                }
                //model.SubscriptionId = id;
                return PartialView("RSSFeeds", model);
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
                model = new FeedViewModel();
                //model.SubscriptionId = id;
                return PartialView("RSSFeeds", model);
            }
        }

    }
}