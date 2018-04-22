namespace PasswordManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUsername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WebsitePasswords", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WebsitePasswords", "UserName");
        }
    }
}
