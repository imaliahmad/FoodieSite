using FoodieSite.BOL;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodieSite.DAL
{
    public interface IRestaurantMasterDb
    {
        IEnumerable<RestaurantMaster> GetAll();
        RestaurantMaster GetById(long id);
        RestaurantMaster Insert(RestaurantMaster obj);
        RestaurantMaster Update(RestaurantMaster obj);
        bool Delete(long id);
    }
    public class RestaurantMasterDb: IRestaurantMasterDb
    {
        private readonly FoodieSiteDbContext context;
        public RestaurantMasterDb(FoodieSiteDbContext _context)
        {
            context = _context;
        }
        public bool Delete(long id)
        {
            var obj = context.RestaurantMaster.Find(id);
            if (obj== null)
            {
                return false;
            }

            context.RestaurantMaster.Remove(obj);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<RestaurantMaster> GetAll()
        {
            return context.RestaurantMaster;
        }

        public RestaurantMaster GetById(long id)
        {
            var obj = context.RestaurantMaster.Find(id);
            return obj;
        }

        public RestaurantMaster Insert(RestaurantMaster obj)
        {
            context.RestaurantMaster.Add(obj);
            context.SaveChanges();
            return obj;
        }

        public RestaurantMaster Update(RestaurantMaster obj)
        {
            context.RestaurantMaster.Update(obj);
            context.SaveChanges();
            return obj;
        }
    }
}
