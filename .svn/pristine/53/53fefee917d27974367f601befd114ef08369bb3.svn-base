namespace Bug_Lite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MimiTypeExpansion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attachments", "ContentType", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Attachments", "ContentType", c => c.String(maxLength: 20));
        }
    }
}
