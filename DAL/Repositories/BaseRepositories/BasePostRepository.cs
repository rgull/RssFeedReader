using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repositories.BaseRepositories
{
    public class BasePostRepository : GenericRepository<Post>
    {
        internal BasePostRepository(ApplicationDbContext db, string userId) : base(db, userId)
        {

        }

        internal BasePostRepository()
        {

        }
    }
}
