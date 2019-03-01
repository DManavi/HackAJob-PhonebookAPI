using System.Data.Entity;

namespace API.Services
{
    public class PhonebookContext : DbContext
    {
        public DbSet<Models.User> Users { get; set; }

        public DbSet<Models.Contact> Contacts { get; set; }

        public DbSet<Models.Field> Fields { get; set; }

        public PhonebookContext() : base(nameOrConnectionString: "PhonebookDB")
        {
        }
    }
}