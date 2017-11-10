using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Users.Dtos;

namespace WIKI.Users
{
    public interface IUserAppService: IApplicationService
    {
        long InsertAndGetId(CreateUserInput dto);
    }
}
