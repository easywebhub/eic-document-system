using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eic.application.Entities;
using eic.application.Entities.Dto;
using eic.core;
using eic.core.Repositories;

namespace eic.application
{
    public class EicMapper : IEicMapper
    {
        private readonly IAccountRepository _accountRepository;

        public EicMapper(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public EicAccount ToEicAccount(Account account)
        {
            return new EicAccount(account, _accountRepository, this);
        }

        public Account ToEntity(Account account, EicAccount eicAccount)
        {
            account.AccountType = eicAccount.AccountType;
            account.Info = eicAccount.Info;
            account.Password = eicAccount.Password;
            account.PasswordSalt = eicAccount.PasswordSaft;
            account.Status = eicAccount.Status;
            account.UserName = eicAccount.UserName;

            return account;
        }

        public EicAccount ToEntity(EicAccount eicAccount, AddAccountDto account)
        {
            eicAccount.AccountType = account.AccountType;
            eicAccount.UserName = account.UserName;
            eicAccount.Info = account.Info;

            return eicAccount;
        }
    }
}
