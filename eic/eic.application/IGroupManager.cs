using System.Collections.Generic;
using eic.application.Dtos;
using eic.application.Entities;

namespace eic.application
{
    public interface IGroupManager
    {
        EicGroup EicGroupAdded { get; }

        bool CreateGroup(CreateGroupDto dto);
        EicGroup GetEicGroup(string id);
        List<EicGroup> GetListGroup();
    }
}