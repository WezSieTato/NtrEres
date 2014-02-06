namespace Eres.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groupTimestamp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "TimeStamp");
            DropColumn("dbo.Subjects", "TimeStamp");
            DropColumn("dbo.Semesters", "TimeStamp");
            DropColumn("dbo.Realisations", "TimeStamp");
            DropColumn("dbo.Registrations", "TimeStamp");
            DropColumn("dbo.GradeValues", "TimeStamp");
            DropColumn("dbo.Grades", "TimeStamp");

            AddColumn("dbo.Groups", "TimeStamp", c => c.Binary(nullable: true, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.GradeValues", "TimeStamp", c => c.Binary(nullable: true, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Registrations", "TimeStamp", c => c.Binary(nullable: true, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Realisations", "TimeStamp", c => c.Binary(nullable: true, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Semesters", "TimeStamp", c => c.Binary(nullable: true, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Subjects", "TimeStamp", c => c.Binary(nullable: true, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Students", "TimeStamp", c => c.Binary(nullable: true, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Grades", "TimeStamp", c => c.Binary(nullable: true, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "TimeStamp", c => c.Binary());
            AlterColumn("dbo.Subjects", "TimeStamp", c => c.Binary());
            AlterColumn("dbo.Semesters", "TimeStamp", c => c.Binary());
            AlterColumn("dbo.Realisations", "TimeStamp", c => c.Binary());
            AlterColumn("dbo.Registrations", "TimeStamp", c => c.Binary());
            AlterColumn("dbo.GradeValues", "TimeStamp", c => c.Binary());
            DropColumn("dbo.Groups", "TimeStamp");
        }
    }
}
