namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Appointments", new[] { "Doctor_Id" });
            DropIndex("dbo.Appointments", new[] { "Patient_Id" });
            DropColumn("dbo.Appointments", "DoctorID");
            DropColumn("dbo.Appointments", "PatientID");
            RenameColumn(table: "dbo.Appointments", name: "Doctor_Id", newName: "DoctorID");
            RenameColumn(table: "dbo.Appointments", name: "Patient_Id", newName: "PatientID");
            AlterColumn("dbo.Appointments", "DoctorID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Appointments", "PatientID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Appointments", "DoctorID");
            CreateIndex("dbo.Appointments", "PatientID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Appointments", new[] { "PatientID" });
            DropIndex("dbo.Appointments", new[] { "DoctorID" });
            AlterColumn("dbo.Appointments", "PatientID", c => c.Int());
            AlterColumn("dbo.Appointments", "DoctorID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Appointments", name: "PatientID", newName: "Patient_Id");
            RenameColumn(table: "dbo.Appointments", name: "DoctorID", newName: "Doctor_Id");
            AddColumn("dbo.Appointments", "PatientID", c => c.Int());
            AddColumn("dbo.Appointments", "DoctorID", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "Patient_Id");
            CreateIndex("dbo.Appointments", "Doctor_Id");
        }
    }
}
