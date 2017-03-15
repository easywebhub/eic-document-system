using System.Collections.Generic;
using eic.application.Dtos;
using eic.application.Entities;

namespace eic.application
{
    public interface IActionManager
    {
        EicAction EicActionAdded { get; }

        bool CreateAction(CreateActionDto dto);
        EicAction GetEwhAction(string id);
        List<EicAction> GetListAction();
    }
}