using EFCore_GraphQL.Models;
using System;
using System.Collections.Concurrent;

namespace EFCore_GraphQL.Services
{
    public interface IPersonEventService
    {
        ConcurrentStack<PersonEvent> AllEvents { get; }
        void AddError(Exception ex);
        PersonEvent AddEvent(PersonEvent personEvent);
        IObservable<PersonEvent> EventStream();
    }
}
