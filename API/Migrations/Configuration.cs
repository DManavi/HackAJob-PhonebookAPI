using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace API.Migrations
{
    internal class Configuration : DbMigrationsConfiguration<API.Services.PhonebookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private static Models.Contact[] GetDefaultContacts()
        {

            Models.Contact[] contacts = new Models.Contact[]
{
                new Models.Contact()
                {
                    Fields = new List<Models.Field>()
                    {
                        new Models.Field()
                        {
                            Category = Models.FieldCategories.PersonalInfo,
                            Attribute = "first-name",
                            Value = "Danial"
                        },
                        new Models.Field()
                        {
                            Category = Models.FieldCategories.PersonalInfo,
                            Attribute = "last-name",
                            Value = "Manavi"
                        },
                        new Models.Field()
                        {
                            Category = Models.FieldCategories.Phone,
                            Attribute = "work",
                            Value = "+989131150815"
                        },
                        new Models.Field()
                        {
                            Category = Models.FieldCategories.Email,
                            Attribute = "email",
                            Value = "dmanavi@live.com"
                        }
                    }
                },
                new Models.Contact()
                {
                    Fields = new List<Models.Field>()
                    {
                        new Models.Field()
                        {
                            Category = Models.FieldCategories.PersonalInfo,
                            Attribute = "first-name",
                            Value = "EMS"
                        },
                        new Models.Field()
                        {
                            Category = Models.FieldCategories.Phone,
                            Attribute = "work",
                            Value = "115"
                        }
                    }
                },
                new Models.Contact()
                {
                    Fields = new List<Models.Field>()
                    {
                        new Models.Field()
                        {
                            Category = Models.FieldCategories.PersonalInfo,
                            Attribute = "first-name",
                            Value = "Police"
                        },
                        new Models.Field()
                        {
                            Category = Models.FieldCategories.Phone,
                            Attribute = "work",
                            Value = "110"
                        }
                    }
                },
};

            return contacts;
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
                   Contacts = new List<Models.Contact>(GetDefaultContacts())
               },

               new Models.User()
               {
                   Username = "user2",
                   Password = "pass2",
                   Contacts = new List<Models.Contact>(GetDefaultContacts())
               },

               new Models.User()
               {
                   Username = "user3",
                   Password = "pass3",
                   Disabled = true,
                   Contacts = new List<Models.Contact>(GetDefaultContacts())
               }
            };

            context.Users.AddOrUpdate(_ => _.Username, users[0]);

            context.SaveChanges();


            base.Seed(context);
        }
    }

    internal class AutoMigrateConfiguration : Configuration
    {
        public AutoMigrateConfiguration() : base()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            var migrator = new DbMigrator(this);

            if (migrator.GetPendingMigrations().Any())
            {
                migrator.Update();

                Seed(new Services.PhonebookContext());
            }
        }
    }
}
