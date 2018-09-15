using DAL.Entities;
using DAL.Models;
using DAL.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PostRepository : BasePostRepository
    {
        public PostRepository()
        {
        }
        /// <summary>
        /// To insert Post, return Id and throw exception if failed to insert.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Post model)
        {
            try
            {
                base.Insert(model);
                base.SaveChanges();
                return model.Id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
