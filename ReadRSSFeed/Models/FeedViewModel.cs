using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadRSSFeed.Models
{
    public class FeedViewModel
    {
        public int SubscriptionId { get; set;}
        public List<RSSFeed> RSSFeeds { get; set; }
        
    }
}