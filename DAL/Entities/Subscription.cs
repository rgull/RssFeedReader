using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Subscription : BaseMainEntity
    {
        public string DisplayName { get; set; }
        public string URL { get; set; }
        public DateTime LastPublishedDate { get; set; }
    }
}
