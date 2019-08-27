using EFCore_GraphQL.GraphQL;
using EFCore_GraphQL.Models;
using EFCore_GraphQL.Services;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore_GraphQL.API
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            Environment = hostingEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //DbContext
            services.AddScoped<EFCore_GraphQLDbContext>();

            //Services
            services.AddSingleton<IPersonService, PersonService>();
            services.AddSingleton<IPersonEventService, PersonEventService>();

            //GraphQL types
            services.AddSingleton<PersonType>();
            services.AddSingleton<PersonStatusEnumType>();
            services.AddSingleton<PhoneNumberType>();
            services.AddSingleton<PhoneNumberClassEnumType>();
            services.AddSingleton<PersonsQuery>();
            services.AddSingleton<PersonCreateInputType>();
            services.AddSingleton<PhoneNumberCreateInputType>();
            services.AddSingleton<PersonsMutation>();
            services.AddSingleton<PersonSubscription>();
            services.AddSingleton<PersonEventType>();
            services.AddSingleton<PersonsSchema>();

            // Add GraphQL services and configure options
            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = Environment.IsDevelopment();
            })
            .AddWebSockets() // Add required services for web socket support
            .AddDataLoader(); // Add required services for DataLoader support

            //Dependency resolver
            services.AddSingleton<IDependencyResolver>(
                    r => new FuncDependencyResolver(type =>
                    r.GetRequiredService(type))
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // we need web sockets for the subscriptions
            app.UseWebSockets();
            app.UseGraphQLWebSockets<PersonsSchema>("/graphql");
            app.UseGraphQL<PersonsSchema>("/graphql");

            //use static files for GraphiQL 
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // enable graphiQL at the root url -- this causes a problem with subscritions...
            //app.UseGraphiQLServer(new GraphiQLOptions() { GraphiQLPath = "/" });

            // use graphql-playground middleware at default url /ui/playground
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            // use voyager middleware at default url /ui/voyager
            app.UseGraphQLVoyager(new GraphQLVoyagerOptions());
        }
    }
}
