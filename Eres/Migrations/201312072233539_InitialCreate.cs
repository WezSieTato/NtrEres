namespace Eres.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MaxValue = c.String(),
                        TimeStamp = c.Binary(),
                        Realisation_RealisationID = c.Int(),
                    })
                .PrimaryKey(t => t.GradeID)
                .ForeignKey("dbo.Realisations", t => t.Realisation_RealisationID)
                .Index(t => t.Realisation_RealisationID);
            
            CreateTable(
                "dbo.GradeValues",
                c => new
                    {
                        GradeValueID = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Date = c.String(),
                        TimeStamp = c.Binary(),
                        Grade_GradeID = c.Int(),
                        Registration_RegistrationID = c.Int(),
                    })
                .PrimaryKey(t => t.GradeValueID)
                .ForeignKey("dbo.Grades", t => t.Grade_GradeID)
                .ForeignKey("dbo.Registrations", t => t.Registration_RegistrationID)
                .Index(t => t.Grade_GradeID)
                .Index(t => t.Registration_RegistrationID);
            
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        RegistrationID = c.Int(nullable: false, identity: true),
                        TimeStamp = c.Binary(),
                        Realisation_RealisationID = c.Int(),
                        Student_StudentID = c.Int(),
                    })
                .PrimaryKey(t => t.RegistrationID)
                .ForeignKey("dbo.Realisations", t => t.Realisation_RealisationID)
                .ForeignKey("dbo.Students", t => t.Student_StudentID)
                .Index(t => t.Realisation_RealisationID)
                .Index(t => t.Student_StudentID);
            
            CreateTable(
                "dbo.Realisations",
                c => new
                    {
                        RealisationID = c.Int(nullable: false, identity: true),
                        TimeStamp = c.Binary(),
                        Semester_SemesterID = c.Int(),
                        Subject_SubjectID = c.Int(),
                    })
                .PrimaryKey(t => t.RealisationID)
                .ForeignKey("dbo.Semesters", t => t.Semester_SemesterID)
                .ForeignKey("dbo.Subjects", t => t.Subject_SubjectID)
                .Index(t => t.Semester_SemesterID)
                .Index(t => t.Subject_SubjectID);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        SemesterID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TimeStamp = c.Binary(),
                    })
                .PrimaryKey(t => t.SemesterID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Conspect = c.String(),
                        url = c.String(),
                        TimeStamp = c.Binary(),
                    })
                .PrimaryKey(t => t.SubjectID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        IndexNo = c.String(),
                        TimeStamp = c.Binary(),
                        Group_GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.Groups", t => t.Group_GroupID)
                .Index(t => t.Group_GroupID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.GroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Registrations", "Student_StudentID", "dbo.Students");
            DropForeignKey("dbo.Students", "Group_GroupID", "dbo.Groups");
            DropForeignKey("dbo.Realisations", "Subject_SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Realisations", "Semester_SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.Registrations", "Realisation_RealisationID", "dbo.Realisations");
            DropForeignKey("dbo.Grades", "Realisation_RealisationID", "dbo.Realisations");
            DropForeignKey("dbo.GradeValues", "Registration_RegistrationID", "dbo.Registrations");
            DropForeignKey("dbo.GradeValues", "Grade_GradeID", "dbo.Grades");
            DropIndex("dbo.Registrations", new[] { "Student_StudentID" });
            DropIndex("dbo.Students", new[] { "Group_GroupID" });
            DropIndex("dbo.Realisations", new[] { "Subject_SubjectID" });
            DropIndex("dbo.Realisations", new[] { "Semester_SemesterID" });
            DropIndex("dbo.Registrations", new[] { "Realisation_RealisationID" });
            DropIndex("dbo.Grades", new[] { "Realisation_RealisationID" });
            DropIndex("dbo.GradeValues", new[] { "Registration_RegistrationID" });
            DropIndex("dbo.GradeValues", new[] { "Grade_GradeID" });
            DropTable("dbo.Groups");
            DropTable("dbo.Students");
            DropTable("dbo.Subjects");
            DropTable("dbo.Semesters");
            DropTable("dbo.Realisations");
            DropTable("dbo.Registrations");
            DropTable("dbo.GradeValues");
            DropTable("dbo.Grades");
        }
    }
}
