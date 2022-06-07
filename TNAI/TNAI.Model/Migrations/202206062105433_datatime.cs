namespace TNAI.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datatime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "DateTime", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "DateTime");
        }
    }
}
