using FoodieSite.BOL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodieSite.DAL
{
    public interface IItemMasterDb
    {
        List<ItemMaster> GetAll(int statusId);
        ItemMaster GetById(long id);
        ItemMaster Insert(ItemMaster obj);
        bool lnsertList(List<ItemMaster> list);
        ItemMaster Update(ItemMaster obj);
        bool Delete(long id);
    }
    public class ItemMasterDb: IItemMasterDb
    {
        private readonly FoodieSiteDbContext context;
        public ItemMasterDb(FoodieSiteDbContext _context)
        {
            context = _context;
        }
        public bool Delete(long id)
        {
            var obj = context.ItemMaster.Find(id);
            if (obj == null)
            {
                return false;
            }

            context.ItemMaster.Remove(obj);
            context.SaveChanges();
            return true;
        }

        public List<ItemMaster> GetAll(int statusId)
        {
            //return 
            //    statusId == 0 ? context.ItemMaster.ToList() :
            //    context.ItemMaster.Where(x=> x.Status == statusId).ToList();

            return context.ItemMaster
                                    .Include(m => m.CategoryMaster)
                                    .Select(x => new ItemMaster() { 
                                          ItemMasterId = x.ItemMasterId,
                                          Name = x.Name,
                                          Description = x.Description,
                                          SalePrice = x.SalePrice,
                                          ImageMasterId = x.ImageMasterId,
                                         // ImageMaster = null,
                                          CategoryMasterId = x.CategoryMasterId,
                                          CategoryMaster =x.CategoryMaster
                                    }).ToList();
        }

        public ItemMaster GetById(long id)
        {
            var obj = context.ItemMaster.Find(id);
            return obj;
        }

        public ItemMaster Insert(ItemMaster obj)
        {
            context.ItemMaster.Add(obj);
            context.SaveChanges();
            return obj;
        }

        public bool lnsertList(List<ItemMaster> list)
        {
            context.ItemMaster.AddRange(list);
            context.SaveChanges();
            return true;
        }

        public ItemMaster Update(ItemMaster obj)
        {
            context.ItemMaster.Update(obj);
            context.SaveChanges();
            return obj;
        }
    }
}
