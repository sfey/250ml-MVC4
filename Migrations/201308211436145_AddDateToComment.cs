namespace _250ml_MVC4_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateToComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "CreationDate");
        }
    }
}
