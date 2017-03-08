using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eic.application;
using eic.application.Entities.Dto;
using ew.common.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace eic.webapi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        /// <summary>
        /// Tạo mới tài khoản
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IActionResult CreateUser([FromBody]AddAccountDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_accountManager.CreateAccount(dto))
            {
                return Ok(_accountManager.EicAccountAdded);
            }
            else
            {
                return ServerError(_accountManager as EwhEntityBase);
            }
        }

    }
}
