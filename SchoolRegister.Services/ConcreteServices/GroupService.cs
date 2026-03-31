using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging; // Zmiana z Castle.Core.Logging
using Microsoft.AspNetCore.Identity;
using SchoolRegister.DAL.EF;
using SchoolRegister.Inter.Services; // Zostawiam zgodnie z Twoim kodem (choć wcześniej było Services.Interfaces)
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices;

public class GroupService : BaseService, IGroupService
{
    private readonly UserManager<User> _userManager;

    public GroupService(
        ApplicationDbContext dbContext, 
        IMapper mapper, 
        ILogger logger, 
        UserManager<User> userManager) 
        : base(dbContext, mapper, logger)
    {
        _userManager = userManager;
    }

    public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
    {
        // TODO: Dodaj logikę dodawania lub aktualizacji grupy
        throw new NotImplementedException();
    }

    public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachStudentToGroupVm)
    {
        // TODO: Dodaj logikę przypisywania ucznia do grupy
        throw new NotImplementedException();
    }

    public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm)
    {
        // TODO: Dodaj logikę przypisywania przedmiotu do grupy
        throw new NotImplementedException();
    }

    public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
    {
        // TODO: Dodaj logikę przypisywania nauczyciela do przedmiotu
        throw new NotImplementedException();
    }

    public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm)
    {
        // TODO: Dodaj logikę usuwania ucznia z grupy
        throw new NotImplementedException();
    }

    public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachDetachSubjectVm)
    {
        // TODO: Dodaj logikę usuwania przedmiotu z grupy
        throw new NotImplementedException();
    }

    public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
    {
        // TODO: Dodaj logikę usuwania nauczyciela z przedmiotu
        throw new NotImplementedException();
    }

    public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
    {
        // TODO: Dodaj logikę pobierania pojedynczej grupy
        throw new NotImplementedException();
    }

    public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filterPredicate = null)
    {
        // TODO: Dodaj logikę pobierania listy grup
        throw new NotImplementedException();
    }
}