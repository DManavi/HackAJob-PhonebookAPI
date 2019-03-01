namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Username = c.String(),
                        Password = c.String(),
                        Disabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserContacts",
                c => new
                    {
                        User_Id = c.Guid(nullable: false),
                        Contact_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Contact_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Contacts", t => t.Contact_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Contact_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserContacts", "Contact_Id", "dbo.Contacts");
            DropForeignKey("dbo.UserContacts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Fields", "Contact_Id", "dbo.Contacts");
            DropIndex("dbo.UserContacts", new[] { "Contact_Id" });
            DropIndex("dbo.UserContacts", new[] { "User_Id" });
            DropIndex("dbo.Fields", new[] { "Contact_Id" });
            DropTable("dbo.UserContacts");
            DropTable("dbo.Users");
            DropTable("dbo.Fields");
            DropTable("dbo.Contacts");
        }
    }
}
