namespace KeJianApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpDate_Study : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Studies", "Del");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Studies", "Del", c => c.String());
        }
    }
}
