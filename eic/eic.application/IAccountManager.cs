using eic.application.Entities;
using eic.application.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.application
{
    public interface IAccountManager
    {
        EicAccount EicAccountAdded { get; }

        bool CreateAccount(CreateAccountDto dto);
        EicAccount GetEwhAccount(string id);
        EicAccount GetEwhAccountByIdSrv(string idSrvAccountId);
        List<EicAccount> GetListAccount();
    }
}
