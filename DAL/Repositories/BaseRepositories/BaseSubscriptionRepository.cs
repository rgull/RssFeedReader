using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repositories.BaseRepositories
{
    public class BaseSubscriptionRepository : GenericRepository<Subscription>
    {
        internal BaseSubscriptionRepository(ApplicationDbContext db, string userId) : base(db, userId)
        {

        }

        internal BaseSubscriptionRepository()
        {

        }
    }
}
