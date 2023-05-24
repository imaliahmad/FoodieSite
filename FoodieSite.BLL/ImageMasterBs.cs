using AutoMapper;
using FoodieSite.BOL;
using FoodieSite.DAL;
using FoodieSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodieSite.BLL
{
    public interface IImageMasterBs
    {
        IEnumerable<ImageMasterVM> GetAll();
        ImageMasterVM GetById(long id);
        ImageMasterVM Insert(ImageMasterVM obj);
        ImageMasterVM Update(ImageMasterVM obj);
        bool Delete(long id);
    }
    public class ImageMasterBs: IImageMasterBs
    {
        private readonly IImageMasterDb objDb;
        public ImageMasterBs(IImageMasterDb _objDb)
        {
            objDb = _objDb;
        }
        public bool Delete(long id)
        {
            return objDb.Delete(id);
        }

        public IEnumerable<ImageMasterVM> GetAll()
        {
            var objList = objDb.GetAll();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImageMaster, ImageMasterVM>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var objListVM = mapper.Map<IEnumerable<ImageMaster>, IEnumerable<ImageMasterVM>>(objList);

            return objListVM;
        }

        public ImageMasterVM GetById(long id)
        {
            var obj = objDb.GetById(id);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImageMaster, ImageMasterVM>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var objVM = mapper.Map<ImageMaster, ImageMasterVM>(obj);

            return objVM;
        }

        public ImageMasterVM Insert(ImageMasterVM objVM)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImageMasterVM, ImageMaster>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var obj = mapper.Map<ImageMasterVM, ImageMaster>(objVM);

            obj = objDb.Insert(obj);

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImageMaster, ImageMasterVM>();
                cfg.IgnoreUnmapped();
            });

            mapper = config.CreateMapper();
            objVM = mapper.Map<ImageMaster, ImageMasterVM>(obj);

            return objVM;
        }

        public ImageMasterVM Update(ImageMasterVM objVM)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImageMasterVM, ImageMaster>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var obj = mapper.Map<ImageMasterVM, ImageMaster>(objVM);

            obj = objDb.Update(obj);

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImageMaster, ImageMasterVM>();
                cfg.IgnoreUnmapped();
            });

            mapper = config.CreateMapper();
            objVM = mapper.Map<ImageMaster, ImageMasterVM>(obj);

            return objVM;
        }
    }
}
