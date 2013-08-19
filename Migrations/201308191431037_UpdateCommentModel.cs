namespace _250ml_MVC4_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCommentModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "UserProfile_UserId", "dbo.UserProfile");
            DropIndex("dbo.Comments", new[] { "UserProfile_UserId" });
            RenameColumn(table: "dbo.Comments", name: "UserProfile_UserId", newName: "UserId");
            AddForeignKey("dbo.Comments", "UserId", "dbo.UserProfile", "UserId", cascadeDelete: true);
            CreateIndex("dbo.Comments", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropForeignKey("dbo.Comments", "UserId", "dbo.UserProfile");
            RenameColumn(table: "dbo.Comments", name: "UserId", newName: "UserProfile_UserId");
            CreateIndex("dbo.Comments", "UserProfile_UserId");
            AddForeignKey("dbo.Comments", "UserProfile_UserId", "dbo.UserProfile", "UserId");
        }
    }
}
