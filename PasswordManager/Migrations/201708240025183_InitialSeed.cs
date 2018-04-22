namespace PasswordManager.Migrations
{
    
    using System.Data.Entity.Migrations;
    
    public partial class InitialSeed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MasterPasswords",
                c => new
                    {
                        MasterPasswordId = c.Int(nullable: false, identity: true),
                        Masterhash = c.String(),
                        Salt = c.Binary(),
                    })
                .PrimaryKey(t => t.MasterPasswordId);
            
            CreateTable(
                "dbo.WebsitePasswords",
                c => new
                    {
                        PasswordId = c.Int(nullable: false, identity: true),
                        SiteName = c.String(),
                        SitePassword = c.String(),
                        Salt = c.String(),
                        IV = c.String(),
                    })
                .PrimaryKey(t => t.PasswordId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WebsitePasswords");
            DropTable("dbo.MasterPasswords");
        }
    }
}
