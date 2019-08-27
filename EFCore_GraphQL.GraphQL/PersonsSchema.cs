using GraphQL;
using GraphQL.Types;

namespace EFCore_GraphQL.GraphQL
{
    public class PersonsSchema : Schema
    {
        public PersonsSchema(PersonsQuery query, PersonsMutation mutation, PersonSubscription subscription, IDependencyResolver resolver)
        {
            Query = query;
            Mutation = mutation;
            Subscription = subscription;
            DependencyResolver = resolver;
        }
    }
}
