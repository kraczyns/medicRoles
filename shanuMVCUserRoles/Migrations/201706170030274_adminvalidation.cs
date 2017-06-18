namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminvalidation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "isValid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "isValid");
        }
    }
}
