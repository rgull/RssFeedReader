using DAL.Entities;
using DAL.Models;
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
    public class SubscriptionController : Controller
    {
        SubscriptionRepository _subscriptionRepo;
        PostRepository _postRepo;
        public SubscriptionController(SubscriptionRepository subscriptionRepo,PostRepository postRepo)
        {
            _subscriptionRepo = subscriptionRepo;
            _postRepo = postRepo;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(SubscriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string currentUserId = HttpContext.User.Identity.GetUserId();
                    if (currentUserId != null)
                    {
                        WebClient wclient = new WebClient();
                        string RSSData = wclient.DownloadString(model.URL);
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
                                           }).ToList();   
                                             
                        TempData["Success"] = "Updated Successfully!";
                        var subId =_subscriptionRepo.Insert(new Subscription { DisplayName = model.DisplayName, URL = model.URL, CreatedBy = currentUserId,LastPublishedDate = sub.FirstOrDefault().LastPublishedDate });
                        foreach (var item in RSSFeedData)
                        {
                            Post post = new Post();
                            post.Author = item.Author;
                            post.Body = item.Description;
                            post.SubscriptionId = subId;
                            post.PostDate = DateTime.Parse(item.PubDate);
                            post.Link = item.Link;
                            post.Title = item.Title;
                            post.CreatedBy = currentUserId;
                            _postRepo.Insert(post);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Log exception
                    TempData["Error"] = ex.Message;
                }
            }
            else
            {
                return View(model);
            }
            return View();
        }
    }
}