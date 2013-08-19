namespace _250ml_MVC4_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHappening : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Happenings",
                c => new
                    {
                        HappeningId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HappeningId);
            
            AddColumn("dbo.Comments", "HappeningId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Comments", "HappeningId", "dbo.Happenings", "HappeningId", cascadeDelete: true);
            CreateIndex("dbo.Comments", "HappeningId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "HappeningId" });
            DropForeignKey("dbo.Comments", "HappeningId", "dbo.Happenings");
            DropColumn("dbo.Comments", "HappeningId");
            DropTable("dbo.Happenings");
        }
    }
}
