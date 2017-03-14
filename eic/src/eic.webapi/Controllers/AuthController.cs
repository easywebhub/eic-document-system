using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eic.application;
using eic.webapi.Dtos;
using eic.common.Enums;
using Microsoft.AspNetCore.Cors;
using ew.common.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace eic.webapi.Controllers
{
    public class AuthController : BaseApiController
    {
        private IAccountManager _accountManager;

        public AuthController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [Route("auth/signin")]
        [HttpPost]
        public IActionResult SignIn([FromBody] SignInRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (dto.SignInByIdSrv)
            {
                var eicAccount = _accountManager.GetEwhAccountByIdSrv(dto.IdSrvAccountId);
                if (eicAccount!=null && eicAccount.CanSignIn())
                {
                    return Ok(eicAccount);
                }
                return ServerError(_accountManager as EwhEntityBase);
            }
            return NoOK();
        }
    }
}
