using eic.application.Dtos;
using eic.application.Entities;
using eic.common.Enums;
using eic.core.Repositories;
using ew.common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.application
{
    public class GroupManager: EwhEntityBase, IGroupManager
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IEicMapper _eicMapper;

        public GroupManager(IGroupRepository groupRepository, IEicMapper eicMapper)
        {
            _groupRepository = groupRepository;
            _eicMapper = eicMapper;
        }

        #region properties
        public EicGroup EicGroupAdded { get; protected set; }
        #endregion

        public bool CreateGroup(CreateGroupDto dto)
        {
            var eicGroup = new EicGroup(_groupRepository, _eicMapper);
            _eicMapper.ToEntity(eicGroup, dto);
            var check = false;
            if (eicGroup.Create())
            {
                check = true;
                EicGroupAdded = eicGroup;
            }
            SyncStatus(this, eicGroup);
            return check;
        }

        public EicGroup GetEicGroup(string id)
        {
            var group = _groupRepository.Get(id);
            if (group == null)
            {
                XStatus = GlobalStatus.NotFound;
                return null;
            }
            return new EicGroup(group, _groupRepository, _eicMapper);
        }

        public List<EicGroup> GetListGroup()
        {
            var groups = _groupRepository.FindAll().ToList();
            var result = new List<EicGroup>();
            result.AddRange(groups.Select(x => _eicMapper.ToEicGroup(x)));
            return result;
        }
    }
}

