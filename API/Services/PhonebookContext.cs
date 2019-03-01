using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace API.Services
{
    public class PhonebookContext : DbContext
    {
        public DbSet<Models.User> Users { get; set; }

        public DbSet<Models.Contact> Contacts { get; set; }

        public DbSet<Models.Field> Fields { get; set; }


        private static PhonebookContext _Instance;

        public static PhonebookContext Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PhonebookContext(true);
                }

                return _Instance;
            }
        }

        public PhonebookContext() : base(nameOrConnectionString: "PhonebookDB") { }

        public PhonebookContext(bool autoMigrate = false) : base(nameOrConnectionString: "PhonebookDB")
        {
            if (autoMigrate)
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhonebookContext, Migrations.AutoMigrateConfiguration>());
            }
        }
    }
}