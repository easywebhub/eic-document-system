using eic.common.Enums;
using eic.core;
using eic.core.Repositories;
using ew.common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.application.Entities
{
    public class EicGroup: EwhEntityBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IEicMapper _eicMapper;

        public EicGroup(IGroupRepository groupRepository, IEicMapper eicMapper)
        {
            _groupRepository = groupRepository;
            _eicMapper = eicMapper;
            _group = new Group();
        }

        public EicGroup(Group group, IGroupRepository groupRepository, IEicMapper eicMapper): this(groupRepository, eicMapper)
        {
            MapFrom(group);
        }

        public EicGroup(string groupId, IGroupRepository groupRepository, IEicMapper eicMapper) : this(groupRepository, eicMapper)
        {
            _group = _groupRepository.Get(groupId);
            MapFrom(_group);
        }

        #region properties
        public string GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Group ParentGroup { get; set; }
        #endregion

        #region ext properties
        private Group _group;
        #endregion

        #region methods
        public bool IsExits()
        {
            if (!string.IsNullOrEmpty(GroupId))
            {
                return true;
            }
            XStatus = GlobalStatus.NotFound;
            return false;
        }
        public bool Create()
        {
            if (Save())
            {
                GroupId = _group.Id;
                SelfSync();
                return true;
            }
            return false;
        }

        public bool Save()
        {
            if (CheckIsIdentity()) //CheckValidModel() && CheckIsIdentity())
            {
                if (_group == null) _group = new Group();
                _groupRepository.AddOrUpdate(_eicMapper.ToEntity(_group, this));
                GroupId = _group.Id;
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
            return check;
        }

        private void MapFrom(Group group)
        {
            if (group == null) return;
            this._group = group;

            this.Name = group.Name;
            this.GroupId = group.Id;
            this.Name = group.Name;
            this.Description = group.Description;
            this.ParentGroup = group.ParentGroup;
        }
        #endregion
    }
}
