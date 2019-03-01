namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Owner_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id, cascadeDelete: true)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Category = c.String(nullable: false),
                        Attribute = c.String(nullable: false),
                        Value = c.String(nullable: false),
                        Contact_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.Contact_Id, cascadeDelete: true)
                .Index(t => t.Contact_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Disabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.Fields", "Contact_Id", "dbo.Contacts");
            DropIndex("dbo.Fields", new[] { "Contact_Id" });
            DropIndex("dbo.Contacts", new[] { "Owner_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Fields");
            DropTable("dbo.Contacts");
        }
    }
}
