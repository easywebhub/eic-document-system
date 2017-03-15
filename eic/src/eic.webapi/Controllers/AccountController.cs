using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eic.application;
using eic.application.Entities.Dto;
using ew.common.Entities;
using eic.middleware.idsrv_wrapper;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace eic.webapi.Controllers
{
    [Route("api/[controller]")]
    public partial class AccountController : BaseApiController
    {
        private IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetUsers()
        {
            return Ok(_accountManager.GetListAccount());
        }

        /// <summary>
        /// Tạo mới tài khoản
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IActionResult CreateUser([FromBody]CreateAccountDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var idSrvManager = new IdSrvManager();
            if (idSrvManager.RegisterUser(new middleware.idsrv_wrapper.Models.RegisterUserDto() { Email = dto.Info.Email, Password = dto.Password, ConfirmPassword = dto.Password }))
            {
                dto.IdSrvAccountId = idSrvManager.UserAdded.UserId;
                if (_accountManager.CreateAccount(dto))
                {
                    return Ok(_accountManager.EicAccountAdded);
                }
                else
                {
                    return ServerError(_accountManager as EwhEntityBase);
                }
            }else
            {
                return ServerError(idSrvManager as EwhEntityBase);
            }
        }

    }
}
