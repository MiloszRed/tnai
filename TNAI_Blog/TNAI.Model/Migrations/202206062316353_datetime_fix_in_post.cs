namespace TNAI.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetime_fix_in_post : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "DateTime", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "DateTime", c => c.String(maxLength: 20));
        }
    }
}
