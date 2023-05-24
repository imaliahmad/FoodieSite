using AutoMapper;
using FoodieSite.BOL;
using FoodieSite.DAL;
using FoodieSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodieSite.BLL
{

    public class NewCategory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual List<NewItem> NewItems { get; set; }
    }
    public class NewItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public virtual NewCategory Category { get; set; }
    }
    public class NewCategoryVM
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual List<NewItem> NewItems { get; set; }
    }
    public class NewItemVM
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public virtual NewCategory Category { get; set; }
    }
    
    public interface IItemMasterBs
    {
        List<ItemMasterVM> GetAll(int statusId);
        ItemMasterVM GetById(long id);
        ItemMasterVM Insert(ItemMasterVM obj);
        bool InsertList(List<ItemMasterVM> list);
        ItemMasterVM Update(ItemMasterVM obj);
        bool Delete(long id);
    }
    public class ItemMasterBs: IItemMasterBs
    {
        private readonly IItemMasterDb objDb;
        public ItemMasterBs(IItemMasterDb _objDb)
        {
            objDb = _objDb;
        }
        public bool Delete(long id)
        {
            return objDb.Delete(id);
        }

        public List<ItemMasterVM> GetAll(int statusId)
        {
            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<NewItem, NewItemVM>();
            //    cfg.CreateMap<NewCategory, NewCategoryVM>();
            //});
            ////  config.AssertConfigurationIsValid();

            //var newSource = new NewItem
            //{
            //    ItemId = 1,
            //    Name = "Orange",
            //    CategoryId = 1,
            //    Category = new NewCategory() { CategoryId = 1, Name = "Fruits" }
            //};

            //var newMapper = config.CreateMapper();
            //var dest1 = newMapper.Map<NewItem, NewItemVM>(newSource);





            List<ItemMaster> objList = objDb.GetAll(statusId);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItemMaster, ItemMasterVM>();
                cfg.CreateMap<CategoryMaster, CategoryMasterVM>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var objListVM = mapper.Map<List<ItemMaster>, List<ItemMasterVM>>(objList);

            return objListVM;

            
        }

        public ItemMasterVM GetById(long id)
        {
            var obj = objDb.GetById(id);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItemMaster, ItemMasterVM>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var objVM = mapper.Map<ItemMaster, ItemMasterVM>(obj);

            return objVM;
        }

        public ItemMasterVM Insert(ItemMasterVM objVM)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItemMasterVM, ItemMaster>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var obj = mapper.Map<ItemMasterVM, ItemMaster>(objVM);

            obj = objDb.Insert(obj); //Db

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItemMaster, ItemMasterVM>();
                cfg.IgnoreUnmapped();
            });

            mapper = config.CreateMapper();
            objVM = mapper.Map<ItemMaster, ItemMasterVM>(obj);

            return objVM;
        }

        public bool InsertList(List<ItemMasterVM> listVM)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItemMasterVM, ItemMaster>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var objList = mapper.Map<List<ItemMasterVM>, List<ItemMaster>>(listVM);

            return objDb.lnsertList(objList);
        }

        public ItemMasterVM Update(ItemMasterVM objVM)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItemMasterVM, ItemMaster>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var obj = mapper.Map<ItemMasterVM, ItemMaster>(objVM);

            obj = objDb.Insert(obj);

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItemMaster, ItemMasterVM>();
                cfg.IgnoreUnmapped();
            });

            mapper = config.CreateMapper();
            objVM = mapper.Map<ItemMaster, ItemMasterVM>(obj);

            return objVM;
        }
    }
}
