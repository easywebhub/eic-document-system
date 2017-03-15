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
    public class EicAction: EwhEntityBase
    {
        private readonly IActionRepository _actionRepository;
        private readonly IEicMapper _eicMapper;

        public EicAction(IActionRepository actionRepository, IEicMapper eicMapper)
        {
            _actionRepository = actionRepository;
            _eicMapper = eicMapper;
            _action = new core.Action();
        }

        public EicAction(core.Action action, IActionRepository actionRepository, IEicMapper eicMapper): this(actionRepository, eicMapper)
        {
            MapFrom(action);
        }

        public EicAction(string actionId, IActionRepository actionRepository, IEicMapper eicMapper): this(actionRepository, eicMapper)
        {
            _action = _actionRepository.Get(actionId);
            MapFrom(_action);
        }

        #region properties
        public string ActionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        #endregion

        #region ext properties
        private core.Action _action;
        #endregion

        #region methods
        public bool IsExits()
        {
            if (!string.IsNullOrEmpty(ActionId))
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
                ActionId = _action.Id;
                SelfSync();
                return true;
            }
            return false;
        }

        public bool Save()
        {
            if (CheckIsIdentity()) //CheckValidModel() && CheckIsIdentity())
            {
                if (_action == null) _action = new core.Action();
                _actionRepository.AddOrUpdate(_eicMapper.ToEntity(_action, this));
                ActionId = _action.Id;
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

        private void MapFrom(core.Action action)
        {
            if (action == null) return;
            this._action = action;

            this.ActionId = action.Id;
            this.Name = action.Name;
            this.Description = action.Description;
        }
        #endregion
    }
}
