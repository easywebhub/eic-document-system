using eic.application.Entities;
using eic.application.Entities.Dto;
using eic.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.application
{
    public interface IEicMapper
    {
        EicAccount ToEicAccount(Account account);
        EicAccount ToEntity(EicAccount eicAccount, AddAccountDto account);
        Account ToEntity(Account account, EicAccount eicAccount);
    }
}
