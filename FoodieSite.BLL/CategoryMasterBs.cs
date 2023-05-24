using AutoMapper;
using FoodieSite.BOL;
using FoodieSite.DAL;
using FoodieSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodieSite.BLL
{
    public interface ICategoryMasterBs
    {
        IEnumerable<CategoryMasterVM> GetAll();
        CategoryMasterVM GetById(long id);
        CategoryMasterVM Insert(CategoryMasterVM obj);
        bool InsertList(List<CategoryMasterVM> list);
        CategoryMasterVM Update(CategoryMasterVM obj);
        bool Delete(long id);
    }
    public class CategoryMasterBs: ICategoryMasterBs
    {
        private readonly ICategoryMasterDb objDb;
        public CategoryMasterBs(ICategoryMasterDb _objDb)
        {
            objDb = _objDb;
        }
        public bool Delete(long id)
        {
            return objDb.Delete(id);
        }

        public IEnumerable<CategoryMasterVM> GetAll()
        {
            var objList = objDb.GetAll();
      
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryMaster, CategoryMasterVM>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var objListVM = mapper.Map<IEnumerable<CategoryMaster>, IEnumerable<CategoryMasterVM>>(objList);

            return objListVM;
        }

        public CategoryMasterVM GetById(long id)
        {
            var obj = objDb.GetById(id);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryMaster, CategoryMasterVM>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var objVM = mapper.Map<CategoryMaster, CategoryMasterVM>(obj);

            return objVM;
        }

        public CategoryMasterVM Insert(CategoryMasterVM objVM)
        {
            //Auto Mapping
            //1. Create configuration
            //2. Create IMapper
            //3. Map

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryMasterVM, CategoryMaster>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var obj = mapper.Map<CategoryMasterVM, CategoryMaster>(objVM);

            obj = objDb.Insert(obj);

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryMaster, CategoryMasterVM>();
                cfg.IgnoreUnmapped();
            });

            mapper = config.CreateMapper();
            objVM = mapper.Map<CategoryMaster, CategoryMasterVM>(obj);

            return objVM;
        }

        public bool InsertList(List<CategoryMasterVM> listVM)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryMasterVM, CategoryMaster>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();

            var list = mapper.Map<List<CategoryMasterVM>, List<CategoryMaster>>(listVM);
            return objDb.lnsertList(list);
        }

        public CategoryMasterVM Update(CategoryMasterVM objVM)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryMasterVM, CategoryMaster>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var obj = mapper.Map<CategoryMasterVM, CategoryMaster>(objVM);

            obj = objDb.Update(obj);

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryMaster, CategoryMasterVM>();
                cfg.IgnoreUnmapped();
            });

            mapper = config.CreateMapper();
            objVM = mapper.Map<CategoryMaster, CategoryMasterVM>(obj);

            return objVM;
        }
    }
}
