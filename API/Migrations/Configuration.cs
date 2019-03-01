using System.Data.Entity.Migrations;

namespace API.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<API.Services.PhonebookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(API.Services.PhonebookContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            Models.User[] users = new Models.User[] {

               new Models.User()
               {
                   Username = "user1",
                   Password = "pass1",
               },

               new Models.User()
               {
                   Username = "user2",
                   Password = "pass2",
               },

               new Models.User()
               {
                   Username = "user3",
                   Password = "pass3",
                   Disabled = true
               }
            };

            context.Users.AddOrUpdate(_ => _.Username, users);
        }
    }
}
