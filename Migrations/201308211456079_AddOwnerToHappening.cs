namespace _250ml_MVC4_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOwnerToHappening : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Happenings", "UserId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Happenings", "UserId", "dbo.UserProfile", "UserId", cascadeDelete: false);
            CreateIndex("dbo.Happenings", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Happenings", new[] { "UserId" });
            DropForeignKey("dbo.Happenings", "UserId", "dbo.UserProfile");
            DropColumn("dbo.Happenings", "UserId");
        }
    }
}
