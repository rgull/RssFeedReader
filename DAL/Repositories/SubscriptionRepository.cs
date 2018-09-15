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
    public class SubscriptionRepository : BaseSubscriptionRepository
    {
        public SubscriptionRepository()
        {
        }
        /// <summary>
        /// To insert Subscription, return Id and throw exception if failed to insert.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Subscription model)
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

        public bool Update(Subscription model)
        {
            try
            {
                var entity = base.GetSingle(x => x.Id == model.Id);
                entity.DisplayName = model.DisplayName;
                entity.URL = model.URL;
                entity.LastPublishedDate = model.LastPublishedDate;               
                base.Update(entity);
                base.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var entity = base.GetSingle(x => x.Id == Id);               
                base.Delete(entity);
                base.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
