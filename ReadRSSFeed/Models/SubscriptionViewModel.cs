using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadRSSFeed.Models
{
    public class SubscriptionViewModel
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string URL { get; set; }
    }
}