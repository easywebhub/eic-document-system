using eic.application.Entities.Dto;
using eic.common.Enums;
using eic.common.Helper;
using eic.core;
using eic.core.Repositories;
using ew.common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eic.application.Entities
{
    public class EicAccount : EwhEntityBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEicMapper _eicMapper;

        public EicAccount(IAccountRepository accountRepository, IEicMapper eicMapper)
        {
            _accountRepository = accountRepository;
            _eicMapper = eicMapper;
        }

        public EicAccount(Account account, IAccountRepository accountRepository, IEicMapper eicMapper) : this(accountRepository, eicMapper)
        {
            MapFrom(account);
        }

        public EicAccount(string accountId, IAccountRepository accountRepository, IEicMapper eicMapper) : this(accountRepository, eicMapper)
        {
            _account = _accountRepository.Get(accountId);
            MapFrom(_account);
        }

        #region properties
        public string IdSrvAccountId { get; set; }
        public string AccountId { get; private set; }
        public string AccountType { get; set; }
        public string Password { get; private set; }
        public string PasswordSaft { get; private set; }
        [Required]
        public string UserName { get; set; }
        public string Status { get; set; }
        public AccountInfo Info { get; set; }
        public List<AccountInGroup> Groups { get; set; }
        public List<ActionOfAccount> Actions { get; set; }
        #endregion

        #region ext properties
        private Account _account;
        #endregion

        #region methods
        public bool IsExits()
        {
            if (!string.IsNullOrEmpty(AccountId))
            {
                return true;
            }
            XStatus = GlobalStatus.NotFound;
            return false;
        }

        public bool CanSignIn()
        {
            if (IsExits())
            {
                if (this.Status == AccountStatus.Active.ToString()) return true;
                else if(this.Status == AccountStatus.Locked.ToString())
                {
                    XStatus = GlobalStatus.Locked;
                }else if(this.Status == AccountStatus.Deleted.ToString())
                {
                    XStatus = GlobalStatus.Deleted;
                }else
                {
                    XStatus = GlobalStatus.NotActiveYet;
                }
            }
            return false;
        }

        public bool Create(AddAccountDto dto)
        {
            _eicMapper.ToEntity(this, dto);
            this.Status = AccountStatus.Active.ToString();
            this.PasswordSaft = StringUtils.CreateSalt(20);
            this.Password = StringUtils.GenerateSaltedHash(dto.Password, this.PasswordSaft);
            return Save();
        }

        public bool Create()
        {
            if (Save())
            {
                AccountId = _account.Id;
                SelfSync();
                return true;
            }
            return false;
        }

        public bool Save()
        {
            if (CheckIsIdentity()) //CheckValidModel() && CheckIsIdentity())
            {
                _accountRepository.AddOrUpdate(_eicMapper.ToEntity(_account ?? new Account(), this));
                return true;
            }
            return false;
        }

        public async Task<bool> SelfSync()
        {
            
            return true;
        }
        #endregion

        #region private methods
        private bool CheckIsIdentity()
        {
            var check = true;
            if (IsExits())
            {
                check = !string.IsNullOrEmpty(this.IdSrvAccountId) && !_accountRepository.FindAll().Any(x => x.Id != this.AccountId && x.IdSrvAccountId == this.IdSrvAccountId);
            }
            else
            {
                check = !string.IsNullOrEmpty(this.IdSrvAccountId) && !_accountRepository.FindAll().Any(x => x.IdSrvAccountId == this.IdSrvAccountId);
            }
            if (!check)
            {
                this.XStatus = GlobalStatus.AlreadyExists;
            }
            return check;
        }

        private void MapFrom(Account account)
        {
            if (account == null) return;
            this._account = account;

            this.IdSrvAccountId = account.IdSrvAccountId;
            this.AccountId = account.Id;
            this.UserName = account.UserName;
            this.AccountType = account.AccountType;
            this.Status = account.Status;
            this.Info = account.Info ?? new AccountInfo();
            this.Password = account.Password;
            this.PasswordSaft = account.PasswordSalt;
            this.Groups = account.Groups;
            this.Actions = account.Actions;
        }
        #endregion
    }
}
