namespace TNAI.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class longer_comments : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Text", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Comments", "Text", c => c.String(nullable: false, maxLength: 300));
        }
    }
}
