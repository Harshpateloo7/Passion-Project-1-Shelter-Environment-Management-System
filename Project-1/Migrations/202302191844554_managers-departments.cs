namespace Project_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class managersdepartments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerID = c.Int(nullable: false, identity: true),
                        ManagerName = c.String(),
                        ManagerBranch = c.String(),
                        ManagerPosition = c.String(),
                    })
                .PrimaryKey(t => t.ManagerID);
            
            CreateTable(
                "dbo.ManagerDepartments",
                c => new
                    {
                        Manager_ManagerID = c.Int(nullable: false),
                        Departments_DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Manager_ManagerID, t.Departments_DepartmentID })
                .ForeignKey("dbo.Managers", t => t.Manager_ManagerID, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.Departments_DepartmentID, cascadeDelete: true)
                .Index(t => t.Manager_ManagerID)
                .Index(t => t.Departments_DepartmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ManagerDepartments", "Departments_DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.ManagerDepartments", "Manager_ManagerID", "dbo.Managers");
            DropIndex("dbo.ManagerDepartments", new[] { "Departments_DepartmentID" });
            DropIndex("dbo.ManagerDepartments", new[] { "Manager_ManagerID" });
            DropTable("dbo.ManagerDepartments");
            DropTable("dbo.Managers");
        }
    }
}
