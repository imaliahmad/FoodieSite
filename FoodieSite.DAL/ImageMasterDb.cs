using FoodieSite.BOL;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodieSite.DAL
{
    public interface IImageMasterDb
    {
        IEnumerable<ImageMaster> GetAll();
        ImageMaster GetById(long id);
        ImageMaster Insert(ImageMaster obj);
        bool lnsertList(List<ImageMaster> list);
        ImageMaster Update(ImageMaster obj);
        bool Delete(long id);
    }
    public class ImageMasterDb: IImageMasterDb
    {
        private readonly FoodieSiteDbContext context;
        public ImageMasterDb(FoodieSiteDbContext _context)
        {
            context = _context;
        }
        public bool Delete(long id)
        {
            var obj = context.ImageMaster.Find(id);
            if (obj == null)
            {
                return false;
            }

            context.ImageMaster.Remove(obj);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<ImageMaster> GetAll()
        {
            return context.ImageMaster;
        }

        public ImageMaster GetById(long id)
        {
            var obj = context.ImageMaster.Find(id);
            return obj;
        }

        public ImageMaster Insert(ImageMaster obj)
        {
            context.ImageMaster.Add(obj);
            context.SaveChanges();
            return obj;
        }

        public bool lnsertList(List<ImageMaster> list)
        {
            context.ImageMaster.AddRange(list);
            context.SaveChanges();
            return true;
        }

        public ImageMaster Update(ImageMaster obj)
        {
            context.ImageMaster.Update(obj);
            context.SaveChanges();
            return obj;
        }
    }
}
