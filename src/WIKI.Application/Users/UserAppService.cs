using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Users.Dtos;

namespace WIKI.Users
{
    public class UserAppService : WIKIAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;

        public UserAppService(IRepository<User, long> userRepository)
        {
            _userRepository = userRepository;
        }

        public long InsertAndGetId(CreateUserInput dto)
        {
            var user = _userRepository.FirstOrDefault(m => m.WUCCUserId == dto.WUCCUserId);
            if (user != null)
                return user.Id;

            var entity = new User { WUCCUserId = dto.WUCCUserId };

            return _userRepository.InsertAndGetId(entity);
        }
    }
}
