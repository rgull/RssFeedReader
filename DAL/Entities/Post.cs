using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
   public class Post: BaseMainEntity
    {        
        public int SubscriptionId { get; set; }
        public string Title { get; set; }
        public DateTime PostDate { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public string  Link { get; set; }

        /// <summary>
        /// Subscription object of this entity.
        /// </summary>
        [ForeignKey("SubscriptionId")]
        public Subscription Subscription { get; set; }

    }
}
