namespace _250ml_MVC4_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        HappeningId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Happenings", t => t.HappeningId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.HappeningId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ratings", new[] { "HappeningId" });
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropForeignKey("dbo.Ratings", "HappeningId", "dbo.Happenings");
            DropForeignKey("dbo.Ratings", "UserId", "dbo.UserProfile");
            DropTable("dbo.Ratings");
        }
    }
}
