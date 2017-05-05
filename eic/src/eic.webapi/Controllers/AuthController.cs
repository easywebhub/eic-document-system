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
using Microsoft.AspNetCore.Authentication;
using ew.middleware.idsrv_wrapper;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace eic.webapi.Controllers
{
    public class AuthController : BaseApiController
    {
        private IAccountManager _accountManager;
        private IIdSrvManager _idSrvManager;

        public AuthController(IAccountManager accountManager, IIdSrvManager idSrvManager )
        {
            _accountManager = accountManager;
            _idSrvManager = idSrvManager;
        }

        /// <summary>
        /// đăng nhập tài khoản trực tiếp ( usernam/password) hoặc gián tiếp qua idsrv ( đăng nhập qua idserver rồi tiếp tục đăng nhập vào hệ thống local = id của user trên idsrv)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("auth/signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!dto.SignInByIdSrv)
            {
                if (_idSrvManager.SignIn(new middleware.idsrv_wrapper.Models.SignInDto() { Username = dto.Username, Password = dto.Password }))
                {
                    dto.IdSrvAccountId = _idSrvManager.UserLoggedFullInfo.UserId;
                }
            }
            if (dto.SignInByIdSrv)
            {
                var eicAccount = _accountManager.GetEwhAccountByIdSrv(dto.IdSrvAccountId);
                if (eicAccount != null)
                {
                    if (eicAccount.CanSignIn())
                    {
                        var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
                        var offlineAccess = await HttpContext.Authentication.GetTokenAsync("refresh_token");
                        return Ok(eicAccount);
                    }
                    else
                    {
                        return ServerError(eicAccount);
                    }
                }
                return ServerError(_accountManager as EwhEntityBase);
            }
            return NoOK();
        }
    }
}
