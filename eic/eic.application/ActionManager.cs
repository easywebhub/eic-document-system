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
    public class ActionManager: EwhEntityBase, IActionManager
    {
        private readonly IActionRepository _actionRepository;
        private readonly IEicMapper _eicMapper;

        public ActionManager(IActionRepository actionRepository, IEicMapper eicMapper)
        {
            _actionRepository = actionRepository;
            _eicMapper = eicMapper;
        }

        #region properties
        public EicAction EicActionAdded { get; protected set; }
        #endregion

        public bool CreateAction(CreateActionDto dto)
        {
            var eicAction = new EicAction(_actionRepository, _eicMapper);
            _eicMapper.ToEntity(eicAction, dto);
            var check = false;
            if (eicAction.Create())
            {
                check = true;
                EicActionAdded = eicAction;
            }
            SyncStatus(this, eicAction);
            return check;
        }

        public EicAction GetEwhAction(string id)
        {
            var action = _actionRepository.Get(id);
            if (action == null)
            {
                XStatus = GlobalStatus.NotFound;
                return null;
            }
            return new EicAction(action, _actionRepository, _eicMapper);
        }

        public List<EicAction> GetListAction()
        {
            var actions = _actionRepository.FindAll().ToList();
            var result = new List<EicAction>();
            result.AddRange(actions.Select(x => _eicMapper.ToEicAction(x)));
            return result;
        }
    }
}

