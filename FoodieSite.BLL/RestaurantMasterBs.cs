using AutoMapper;
using FoodieSite.BOL;
using FoodieSite.DAL;
using FoodieSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodieSite.BLL
{
    public interface IRestaurantMasterBs
    {
        IEnumerable<RestaurantMasterVM> GetAll();
        RestaurantMasterVM GetById(long id);
        RestaurantMasterVM Insert(RestaurantMasterVM objVM);
        RestaurantMasterVM Update(RestaurantMasterVM objVM);
        bool Delete(long id);
    }
    public class RestaurantMasterBs: IRestaurantMasterBs
    {
        private readonly IRestaurantMasterDb objDb;
        public RestaurantMasterBs(IRestaurantMasterDb _objDb)
        {
            objDb = _objDb;
        }
        public bool Delete(long id)
        {
            return objDb.Delete(id);
        }

        public IEnumerable<RestaurantMasterVM> GetAll()
        {
            var objList = objDb.GetAll();

            //Auto Mapping
            //1. Create configuration
            //2. Create IMapper
            //3. Map

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RestaurantMaster, RestaurantMasterVM>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var objListVM = mapper.Map<IEnumerable<RestaurantMaster>, IEnumerable<RestaurantMasterVM>>(objList);

            return objListVM;
        }

        public RestaurantMasterVM GetById(long id)
        {
            var obj = objDb.GetById(id);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RestaurantMaster, RestaurantMasterVM>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var objVM = mapper.Map<RestaurantMaster, RestaurantMasterVM>(obj);

            return objVM;
        }

        public RestaurantMasterVM Insert(RestaurantMasterVM objVM)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RestaurantMasterVM, RestaurantMaster>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var obj = mapper.Map<RestaurantMasterVM, RestaurantMaster>(objVM);

            obj = objDb.Insert(obj); //Db Call

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RestaurantMasterVM, RestaurantMasterVM>();
                cfg.IgnoreUnmapped();
            });

            mapper = config.CreateMapper();
            objVM = mapper.Map<RestaurantMaster, RestaurantMasterVM>(obj);

            return objVM;
        }

        public RestaurantMasterVM Update(RestaurantMasterVM objVM)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RestaurantMasterVM, RestaurantMaster>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var obj = mapper.Map<RestaurantMasterVM, RestaurantMaster>(objVM);

            obj = objDb.Update(obj);

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RestaurantMaster, RestaurantMasterVM>();
                cfg.IgnoreUnmapped();
            });

            mapper = config.CreateMapper();
            objVM = mapper.Map<RestaurantMaster, RestaurantMasterVM>(obj);

            return objVM;
        }
    }
}
