using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using EFCore_GraphQL.Models;
using EFCore_GraphQL.Services;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;

namespace EFCore_GraphQL.GraphQL
{
    public class PersonSubscription : ObjectGraphType<object>
    {
        private IPersonEventService _personEventService;

        public PersonSubscription(IPersonEventService personEventService)
        {
            _personEventService = personEventService;

            Name = "Subscription";

            AddField(new EventStreamFieldType
            {
                Name = "personEvent",
                Arguments = new QueryArguments(
                    new QueryArgument<ListGraphType<PersonStatusEnumType>>
                    {
                        Name="statuses"
                    }),
                Type = typeof(PersonEventType),
                Resolver = new FuncFieldResolver<PersonEvent>(ResolveEvent),
                Subscriber = new EventStreamResolver<PersonEvent>(Subscribe)
            });
        }

        private IObservable<PersonEvent> Subscribe(ResolveEventStreamContext arg)
        {
            var statusList = arg.GetArgument<IList<PersonStatusEnum>>(
                "statuses",
                new List<PersonStatusEnum>()
                );

            if (statusList.Count > 0)
            {
                PersonStatusEnum statuses = 0;

                foreach (var status in statusList)
                {
                    statuses = statuses | status;
                }
                return _personEventService.EventStream()
                    .Where(e => (e.Status & statuses) == e.Status);
            }
            else
                return _personEventService.EventStream();
        }

        private PersonEvent ResolveEvent(ResolveFieldContext arg)
        {
            var personEvent = arg.Source as PersonEvent;
            return personEvent;
        }
    }
}
