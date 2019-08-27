using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using EFCore_GraphQL.Models;

namespace EFCore_GraphQL.Services
{
    public class PersonEventService : IPersonEventService
    {
        private readonly ISubject<PersonEvent> _eventStream =
            new ReplaySubject<PersonEvent>();

        public ConcurrentStack<PersonEvent> AllEvents { get; }

        public PersonEventService()
        {
            AllEvents = new ConcurrentStack<PersonEvent>();
        }

        public void AddError(Exception ex)
        {
            _eventStream.OnError(ex);
        }

        public PersonEvent AddEvent(PersonEvent personEvent)
        {
            AllEvents.Push(personEvent);
            _eventStream.OnNext(personEvent);
            return personEvent;
        }

        public IObservable<PersonEvent> EventStream()
        {
            return _eventStream.AsObservable();
        }
    }
}
