namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentuserchanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Specialization", c => c.Int());
            AddColumn("dbo.AspNetUsers", "PESEL", c => c.String());
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AlterColumn("dbo.Appointments", "PatientID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "PatientID", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.AspNetUsers", "PESEL");
            DropColumn("dbo.AspNetUsers", "Specialization");
        }
    }
}
