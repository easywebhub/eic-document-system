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
            account.IdSrvAccountId = eicAccount.IdSrvAccountId;
            account.Info = eicAccount.Info;
            account.Password = eicAccount.Password;
            account.PasswordSalt = eicAccount.PasswordSaft;
            account.Status = eicAccount.Status;
            account.UserName = eicAccount.UserName;
            account.Groups = eicAccount.Groups;
            account.Actions = eicAccount.Actions;
            return account;
        }

        public EicAccount ToEntity(EicAccount eicAccount, AddAccountDto account)
        {
            eicAccount.IdSrvAccountId = account.IdSrvAccountId;
            eicAccount.AccountType = account.AccountType;
            eicAccount.UserName = account.UserName;
            eicAccount.Info = account.Info;
            

            return eicAccount;
        }

        public EicAccount ToEntity(EicAccount eicAccount, CreateAccountDto account)
        {
            eicAccount.IdSrvAccountId = account.IdSrvAccountId;
            eicAccount.AccountType = account.AccountType;
            eicAccount.UserName = account.UserName;
            eicAccount.Info = account.Info;
            eicAccount.Groups = account.Groups;
            eicAccount.Actions = account.Actions;

            return eicAccount;
        }

        #region group
        public Group ToEntity(Group group, EicGroup eicGroup)
        {
            group.Name = eicGroup.Name;
            group.Description = eicGroup.Description;
            group.ParentGroup = eicGroup.ParentGroup;
            return group;
        }
        #endregion

        #region action
        public core.Action ToEntity(core.Action action, EicAction eicAction)
        {
            action.Name = eicAction.Name;
            action.Description = eicAction.Description;
            return action;
        }
        #endregion
    }
}
