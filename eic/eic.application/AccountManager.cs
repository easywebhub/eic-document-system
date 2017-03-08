using ew.common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eic.application.Entities;
using eic.core.Repositories;
using eic.common.Enums;
using eic.application.Entities.Dto;

namespace eic.application
{
    public class AccountManager : EwhEntityBase, IAccountManager
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEicMapper _eicMapper;

        public AccountManager(IAccountRepository accountRepository, IEicMapper eicMapper)
        {
            _accountRepository = accountRepository;
            _eicMapper = eicMapper;
        }

        #region properties
        public EicAccount EicAccountAdded { get; protected set; }
        #endregion

        public bool CreateAccount(AddAccountDto dto)
        {
            var ewhAccount = new EicAccount(_accountRepository, _eicMapper);
            var check = false;
            if (ewhAccount.Create(dto))
            {
                check = true;
                EicAccountAdded = ewhAccount;
            }
            SyncStatus(this, ewhAccount);
            return check;
        }

        public EicAccount GetEwhAccount(string id)
        {
            var account = _accountRepository.Get(id);
            if (account == null)
            {
                XStatus = GlobalStatus.NotFound;
                return null;
            }
            return new EicAccount(account, _accountRepository, _eicMapper);
        }

        public List<EicAccount> GetListAccount()
        {
            var accounts = _accountRepository.FindAll().ToList();
            var result = new List<EicAccount>();
            result.AddRange(accounts.Select(x => _eicMapper.ToEicAccount(x)));
            return result;
        }
    }
}
