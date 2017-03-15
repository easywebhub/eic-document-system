using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eic.application;
using eic.application.Dtos;
using ew.common.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace eic.webapi.Controllers
{
    public class ActionController : BaseApiController
    {
        private readonly IActionManager _actionManager;
        private readonly IEicMapper _eicMapper;

        public ActionController(IActionManager actionManager, IEicMapper eicMapper)
        {
            _actionManager = actionManager;
            _eicMapper = eicMapper;
        }

        [HttpGet]
        [Route("actions")]
        public IActionResult GetActions()
        {
            return Ok(_actionManager.GetListAction());
        }

        [HttpPost]
        [Route("actions")]
        public IActionResult CreateAction([FromBody] CreateActionDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_actionManager.CreateAction(dto))
            {
                return Ok(_actionManager.EicActionAdded);
            }else
            {
                return ServerError(_actionManager as EwhEntityBase);
            }
        }
    }
}
