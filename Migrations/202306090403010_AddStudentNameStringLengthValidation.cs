namespace MVCDemoProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentNameStringLengthValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "StudentName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "StudentName", c => c.String(nullable: false));
        }
    }
}