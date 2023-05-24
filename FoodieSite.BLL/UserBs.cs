using AutoMapper;
using FoodieSite.BOL;
using FoodieSite.DAL;
using FoodieSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodieSite.BLL
{
    public interface IUserBs
    {
        Task<EditProfileVM> Update(EditProfileVM obj);
    }
    public class UserBs: IUserBs
    {
        private IUserDb objUserDb;
        public UserBs(IUserDb _objUserDb)
        {
            objUserDb = _objUserDb;
        }
        public async Task<EditProfileVM> Update(EditProfileVM objVM)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditProfileVM, AppUsers>();
                cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();
            var obj = mapper.Map<EditProfileVM, AppUsers>(objVM);

            obj = await objUserDb.Update(obj);

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AppUsers, EditProfileVM>();
                cfg.IgnoreUnmapped();
            });

            mapper = config.CreateMapper();
            objVM = mapper.Map<AppUsers, EditProfileVM>(obj);

            return objVM;
        }
    }
}
