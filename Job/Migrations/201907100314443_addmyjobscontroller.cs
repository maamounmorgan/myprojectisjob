namespace Job.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmyjobscontroller : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        JobContent = c.String(),
                        JobImage = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyJobs", "CategoryId", "dbo.Categories");
            DropIndex("dbo.MyJobs", new[] { "CategoryId" });
            DropTable("dbo.MyJobs");
        }
    }
}
