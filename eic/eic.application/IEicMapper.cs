using eic.application.Dtos;
using eic.application.Entities;
using eic.application.Entities.Dto;
using eic.core;

namespace eic.application
{
    public interface IEicMapper
    {
        EicAccount ToEicAccount(Account account);
        EicAction ToEicAction(Action action);
        EicGroup ToEicGroup(Group group);
        Group ToEntity(Group group, EicGroup eicGroup);
        EicAccount ToEntity(EicAccount eicAccount, AddAccountDto account);
        EicAccount ToEntity(EicAccount eicAccount, CreateAccountDto account);
        Action ToEntity(Action action, EicAction eicAction);
        Account ToEntity(Account account, EicAccount eicAccount);
        EicAction ToEntity(EicAction eicAction, CreateActionDto dto);
        EicGroup ToEntity(EicGroup eicGroup, CreateGroupDto dto);
    }
}