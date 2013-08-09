namespace ChecklistManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChecklistTemplates",
                c => new
                    {
                        ChecklistTemplateId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.ChecklistTemplateId);
            
            CreateTable(
                "dbo.CheckItemTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsDone = c.Boolean(nullable: false),
                        ChecklistTemplateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChecklistTemplates", t => t.ChecklistTemplateId, cascadeDelete: true)
                .Index(t => t.ChecklistTemplateId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CheckItemTemplates", new[] { "ChecklistTemplateId" });
            DropForeignKey("dbo.CheckItemTemplates", "ChecklistTemplateId", "dbo.ChecklistTemplates");
            DropTable("dbo.CheckItemTemplates");
            DropTable("dbo.ChecklistTemplates");
        }
    }
}
