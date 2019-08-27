using EFCore_GraphQL.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace EFCore_GraphQL.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            //Seed(host);
            host.Run();
        }

        //<summary>
        //  Seed the given host. Used it in the very beggining of the practice to insert 
        //  one record so the process of building the query had some test data to test it properly
        //</summary>
        //<remarks>
        //  Ralfaro, 8/1/2019.
        //</remarks>
        //<param name="host">
        //  The host.
        //</param>
        private static void Seed(IWebHost host)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<EFCore_GraphQLDbContext>();
                context.Database.EnsureDeleted();
                context.Database.Migrate();

                var person = new Person
                {
                    Name = "William",
                    LastName = "Shakespeare"
                };
                person.PhoneNumbers = new List<PhoneNumber>() {
                        new PhoneNumber()
                        {
                            Owner = person,
                            Number = "1111 111 111",
                            PhoneNumberClass = PhoneNumberClassEnum.HOME,
                            Description = "A home phone number"

                        },
                        new PhoneNumber()
                        {
                            Owner = person,
                            Number = "2222 222 222",
                            PhoneNumberClass = PhoneNumberClassEnum.MOBILE,
                            Description = "A mobile phone number"
                        },
                        new PhoneNumber()
                        {
                            Owner = person,
                            Number = "3333 333 333",
                            PhoneNumberClass = PhoneNumberClassEnum.WORK,
                            Description = "A work phone number"
                        }
                };

                context.Add(person);
                context.SaveChanges();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
