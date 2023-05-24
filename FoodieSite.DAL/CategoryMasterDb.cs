using FoodieSite.BOL;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodieSite.DAL
{
    public interface ICategoryMasterDb
    {
        IEnumerable<CategoryMaster> GetAll();
        CategoryMaster GetById(long id);
        CategoryMaster Insert(CategoryMaster obj);
        bool lnsertList(List<CategoryMaster> list);
        CategoryMaster Update(CategoryMaster obj);
        bool Delete(long id);
    }
    public class CategoryMasterDb: ICategoryMasterDb
    {
        private readonly FoodieSiteDbContext context;
        public CategoryMasterDb(FoodieSiteDbContext _context)
        {
            context = _context;
        }
        public bool Delete(long id)
        {
            var obj = context.CategoryMaster.Find(id);
            if (obj == null)
            {
                return false;
            }

            context.CategoryMaster.Remove(obj);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<CategoryMaster> GetAll()
        {
            return context.CategoryMaster;
        }

        public CategoryMaster GetById(long id)
        {
            var obj = context.CategoryMaster.Find(id);
            return obj;
        }

        public CategoryMaster Insert(CategoryMaster obj)
        {
            context.CategoryMaster.Add(obj);
            context.SaveChanges();
            return obj;
        }

        public bool lnsertList(List<CategoryMaster> list)
        {
            context.CategoryMaster.AddRange(list);
            context.SaveChanges();
            return true;
        }

        public CategoryMaster Update(CategoryMaster obj)
        {
            context.CategoryMaster.Update(obj);
            context.SaveChanges();
            return obj;
        }
    }
}
