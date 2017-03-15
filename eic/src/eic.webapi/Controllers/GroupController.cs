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
    public class GroupController : BaseApiController
    {
        private readonly IGroupManager _groupManager;
        private readonly IEicMapper _eicMapper;

        public GroupController(IGroupManager groupManager, IEicMapper eicMapper)
        {
            _groupManager = groupManager;
            _eicMapper = eicMapper;
        }

        [HttpGet]
        [Route("groups")]
        public IActionResult GetGroups()
        {
            return Ok(_groupManager.GetListGroup());
        }

        [HttpPost]
        [Route("groups")]
        public IActionResult CreateGroup([FromBody] CreateGroupDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_groupManager.CreateGroup(dto))
            {
                return Ok(_groupManager.EicGroupAdded);
            }else
            {
                return ServerError(_groupManager as EwhEntityBase);
            }
        }
    }
}
